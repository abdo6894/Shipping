// نقرأ shipmentId من الـ URL
const urlParams = new URLSearchParams(window.location.search);
const shipmentId = urlParams.get("shipmentId");
let currentShipment = null;

// نحمّل بيانات الشحنة أول ما الصفحة تفتح
document.addEventListener("DOMContentLoaded", async () => {
    if (!shipmentId) {
        console.error("No shipmentId in URL");
        return;
    }

    try {
        const res = await fetch(ApiClient.baseUrl + `/api/Shipments/${shipmentId}`);
        if (!res.ok) {
            const txt = await res.text();
            console.error("Failed to load shipment:", res.status, txt);
            return;
        }

        const apiResponse = await res.json();   // ApiResponse<ShipmentDto>
        currentShipment = apiResponse.data || apiResponse.Data;
        console.log("Loaded shipment for payment:", currentShipment);
    } catch (err) {
        console.error("Error loading shipment:", err);
    }
});

// أزرار PayPal الرسمية
const paypalButtons = window.paypal.Buttons({
    style: {
        shape: "rect",
        layout: "vertical",
        color: "blue",
        label: "buynow"
    },

    // إنشاء الأوردر في السيرفر
    async createOrder() {
        if (!currentShipment) {
            throw new Error("Shipment not loaded yet");
        }

        // المبلغ من ShipingRate
        const total = currentShipment.ShipingRate;

        const payload = {
            orderId: currentShipment.Id,   // نربطه بالشحنة لو حابب
            amount: total,
            shippingValue: 0
        };

        const res = await fetch(ApiClient.baseUrl + "/api/Payment/create-order", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(payload)
        });

        if (!res.ok) {
            const text = await res.text();
            throw new Error(`create-order failed ${res.status}: ${text}`);
        }

        const data = await res.json();
        if (!data?.id) {
            throw new Error(`create-order: missing id in response: ${JSON.stringify(data)}`);
        }

        // لازم ترجع الـ id للـ PayPal SDK
        return data.id;
    },

    // بعد الموافقة والدفع
    async onApprove(data) {
        // data.orderID جاي من PayPal
        const res = await fetch(ApiClient.baseUrl + "/api/Payment/capture-order", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify({ OrderId: data.orderID, Amount: 0 })
        });

        if (!res.ok) {
            const text = await res.text();
            throw new Error(`capture-order failed ${res.status}: ${text}`);
        }

        const capture = await res.json();
        console.log("PayPal capture result:", capture);

        // نعلّم الشحنة كـ Paid
        if (shipmentId) {
            const markPaidRes = await fetch(ApiClient.baseUrl + "/api/Shipments/MarkPaid", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json"
                },
                body: JSON.stringify({
                    id: shipmentId,             // ShipmentDto.Id
                    isPaid: true,
                    paymentGateway: "PayPal",
                    paymentReference: data.orderID
                })
            });

            if (!markPaidRes.ok) {
                const txt = await markPaidRes.text();
                console.error("MarkPaid failed:", markPaidRes.status, txt);
            }
        }

        alert("Transaction completed!");
        // رجوع لليست الشحنات
        window.location.href = "/Shipment/List";
    },

    onError(err) {
        console.error("PayPal Buttons Error:", err);
        alert("Error while processing PayPal payment.");
    }
});

// رندر في الديف الرسمي
paypalButtons.render("#paypal-button-container");
