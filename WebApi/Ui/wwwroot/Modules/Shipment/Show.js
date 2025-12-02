$(document).ready(function () {
    const shipmentId = AppHelper.getQueryParam("id") || AppHelper.getIdFromPath();
    console.log("Edit Id from URL:", shipmentId); // هنا

    if (shipmentId) {
        LoadShipmentById(shipmentId);
    }
});

function LoadShipmentById(id) {
    ShipmentService.GetById(
        id,
        function (response) {

            console.log("🔍 Raw response from /api/Shipments/{id}:", response);

            const data = response.Data ?? response.data;
            console.log("🔍 Extracted data sent to FillShipmentForm:", data);

            ShipmentService.FillShipmentForm(data);

        },
        function (error) {
            console.error("API Error", error);
            alert("فشل في تحميل بيانات الشحنة");
        }
    );
}
