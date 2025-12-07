const stripe = Stripe("pk_test_51SaxK7C7IE9PGI1XUfHzOjCoJxeoGXY2l6t2JopdilgjL1L5rSdezH3j0aVCMWu5AbcVqx2QfsWUAnjSR48LgqKc00Q4Q3R8k4");

document.addEventListener("DOMContentLoaded", async () => {
    const checkoutButton = document.getElementById("checkout-button");
    const resultMessage = document.getElementById("stripe-result-message");

    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get("status");
    const shipmentId = urlParams.get("shipmentId");

    // لو رجعنا من Stripe بعد ما خلّص الدفع أو كانسل
    if (status === "success") {
        if (shipmentId) {
            try {
                const markPaidRes = await fetch(ApiClient.baseUrl + "/api/Shipments/MarkPaid", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        "Accept": "application/json"
                    },
                    body: JSON.stringify({
                        id: shipmentId,
                        isPaid: true,
                        paymentGateway: "Stripe",
                        paymentReference: urlParams.get("session_id") || ""
                    })
                });

                if (!markPaidRes.ok) {
                    const txt = await markPaidRes.text();
                    console.error("MarkPaid (Stripe) failed:", markPaidRes.status, txt);
                } else {
                    AppHelper.showToast("تم الدفع بنجاح عبر Stripe.", "success");
                    if (resultMessage) resultMessage.textContent = "تم الدفع بنجاح.";
                }
            } catch (err) {
                console.error("Error calling MarkPaid (Stripe):", err);
            }
        }

        window.location.href = "/Shipment/List";
        return;
    } else if (status === "cancel") {
        AppHelper.showToast("تم إلغاء عملية الدفع.", "info");
        if (resultMessage) resultMessage.textContent = "تم إلغاء عملية الدفع.";
        return;
    }

    if (!checkoutButton) return;

    // حمّل الشحنة عشان نستخدم ShipingRate
    let currentShipment = null;
    if (shipmentId) {
        try {
            const res = await fetch(ApiClient.baseUrl + `/api/Shipments/${shipmentId}`);
            if (res.ok) {
                const apiResponse = await res.json();            // ApiResponse<ShipmentDto>
                currentShipment = apiResponse.data || apiResponse.Data;
                console.log("Loaded shipment for Stripe:", currentShipment);
            } else {
                const txt = await res.text();
                console.error("Failed to load shipment for Stripe:", res.status, txt);
            }
        } catch (err) {
            console.error("Error loading shipment for Stripe:", err);
        }
    }

    checkoutButton.addEventListener("click", async () => {
        try {
            if (!currentShipment) {
                if (resultMessage) resultMessage.textContent = "تعذر تحميل بيانات الشحنة.";
                return;
            }

            const payload = {
                shipmentId: shipmentId,
                orderId: currentShipment.Id,
                amount: currentShipment.ShipingRate   // هنا المبلغ = ShipingRate
            };

            const res = await fetch(ApiClient.baseUrl + "/api/Payment/create-checkout-session", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify(payload)
            });

            if (!res.ok) {
                const text = await res.text();
                console.error("create-checkout-session failed:", res.status, text);
                if (resultMessage) resultMessage.textContent = "فشل في إنشاء جلسة الدفع.";
                return;
            }

            const data = await res.json();
            if (!data.sessionId) {
                console.error("missing sessionId:", data);
                if (resultMessage) resultMessage.textContent = "استجابة غير متوقعة من خادم الدفع.";
                return;
            }

            const result = await stripe.redirectToCheckout({
                sessionId: data.sessionId
            });

            if (result.error) {
                console.error(result.error.message);
                if (resultMessage) resultMessage.textContent = result.error.message || "خطأ أثناء التحويل إلى بوابة الدفع.";
            }


            alert("Transaction completed!");
            // رجوع لليست الشحنات
            window.location.href = "/Shipment/List";
        } catch (err) {
            console.error("Stripe checkout error:", err);
            if (resultMessage) resultMessage.textContent = "حدث خطأ أثناء بدء الدفع.";
        }
    });
});
