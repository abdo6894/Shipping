document.addEventListener("DOMContentLoaded", async () => {
    console.log("Paymob page loaded, href:", window.location.href);

    const urlParams = new URLSearchParams(window.location.search);
    const shipmentId = urlParams.get("shipmentId");
    const success = urlParams.get("success");
    const txnCode = urlParams.get("txn_response_code");
    const txnId = urlParams.get("id"); // transaction id من Paymob لو عايز تخزّنه

    console.log("Query params:", {
        shipmentId,
        success,
        txnCode,
        txnId
    });

    const paymobButton = document.getElementById("paymob-button");
    const resultMsg = document.getElementById("paymob-result-message");

    console.log("paymobButton:", paymobButton);
    console.log("resultMsg:", resultMsg);

    if (!paymobButton) {
        console.warn("paymob-button not found in DOM, exiting script.");
        return;
    }

    // --- 🛠️ التعديل يبدأ هنا: نعتمد على Paymob Webhook بدلاً من استدعاء MarkPaid من العميل ---
    // ========= لو راجع من Paymob بعد الدفع (أو الفشل) =========
    if (shipmentId && (success === "true" || success === "false" || txnCode)) {

        console.log(`Returned from Paymob. Success: ${success}. Relying on Paymob Webhook to update status.`);

        if (resultMsg) {
            if (success === "true") {
                resultMsg.textContent = "✅ تم استلام طلب الدفع بنجاح. جاري التحقق من حالة الشحنة...";
                resultMsg.style.color = "#00a19a";
            } else {
                resultMsg.textContent = "❌ فشلت عملية الدفع. يرجى مراجعة التفاصيل أو المحاولة مرة أخرى.";
                resultMsg.style.color = "#d93025";
            }
        }

        // إزالة زر الدفع بعد العودة لضمان عدم الضغط عليه مرة أخرى بالخطأ
        if (paymobButton) {
            paymobButton.style.display = 'none';
        }

        // التحويل لصفحة القوائم بعد تأخير بسيط للسماح للمستخدم بقراءة الرسالة
        // Paymob Webhook سيعمل في الخلفية ويحدث حالة الشحنة على السيرفر
        console.log("Redirecting to /Shipment/List in 3 seconds...");
        setTimeout(() => {
            window.location.href = "/Shipment/List";
        }, 3000); // تحويل بعد 3 ثواني

        return;
    }

    // --- 🛠️ التعديل ينتهي هنا ---

    let currentShipment = null;

    // 1) تحميل الشحنة باستخدام ApiClient (فقط إذا لم يكن قد عاد من عملية دفع)
    if (shipmentId) {
        console.log("Loading shipment from API, id:", shipmentId);

        ApiClient.get(`/api/Shipments/${shipmentId}`,
            function (response) {
                // ApiResponse<ShipmentDto>
                currentShipment = response?.Data || response?.data || null;
                console.log("Loaded shipment for Paymob:", currentShipment);

                if (!currentShipment && resultMsg) {
                    resultMsg.textContent = "تعذر تحميل بيانات الشحنة.";
                }
            },
            function (xhr) {
                console.error("Failed to load shipment for Paymob:", xhr);
                if (resultMsg) resultMsg.textContent = "تعذر تحميل بيانات الشحنة.";
            },
            true // useAuth: يضمن إرسال Authorization
        );
    } else {
        console.warn("No shipmentId in query string, skipping shipment load.");
    }

    // 2) بدء الدفع مع Paymob عند الضغط على الزر
    paymobButton.addEventListener("click", async () => {
        console.log("Paymob button clicked, currentShipment:", currentShipment);

        try {
            if (!currentShipment) {
                console.warn("currentShipment is null, cannot start payment.");
                if (resultMsg) resultMsg.textContent = "بيانات الشحنة غير متاحة.";
                return;
            }

            const payload = {
                orderId: currentShipment.Id,
                amount: currentShipment.ShipingRate
            };

            console.log("Create-order payload:", payload);

            const res = await fetch(ApiClient.baseUrl + "/api/Payment/create-order", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify(payload)
            });

            console.log("create-order status:", res.status);

            if (!res.ok) {
                const txt = await res.text();
                console.error("Paymob create-order failed:", res.status, txt);
                if (resultMsg) resultMsg.textContent = "فشل في إنشاء طلب الدفع مع Paymob.";
                return;
            }

            const data = await res.json();
            console.log("create-order response JSON:", data);

            if (!data.id) {
                console.error("Paymob response missing id:", data);
                if (resultMsg) resultMsg.textContent = "استجابة غير متوقعة من خادم الدفع.";
                return;
            }

            const iframeUrl = data.id;
            console.log("Redirecting to Paymob iframe URL:", iframeUrl);
            window.location.href = iframeUrl;
        } catch (err) {
            console.error("Paymob error:", err);
            if (resultMsg) resultMsg.textContent = "حدث خطأ أثناء بدء الدفع مع Paymob.";
        }
    });
});