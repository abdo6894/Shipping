
const ShipmentService = {
    FormIds: {},
    GetModel: function () {
        const shipmentDto = {
            ShipingDate: new Date().toISOString(),
            DelivryDate: new Date(new Date().setDate(new Date().getDate() + 3)).toISOString(),

            SenderId: "00000000-0000-0000-0000-000000000000",
            SenderData: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                SenderName: $('input[name="SenderName"]').val(),
                Email: $('input[name="Email"]').val(),
                Phone: $('input[name="Phone"]').val(),
                CityId: $('select[name="SenderCityId"]').val(),
                Address: $('input[name="Address"]').val(),
                Contact: $('input[name="Contact"]').val(),
                PostalCode: $('input[name="PostalCode"]').val(),
                OtherAddress: $('input[name="OtherAddress"]').val()
            },

            ReceiverId: "00000000-0000-0000-0000-000000000000",
            ReciverData: {
                Id: "00000000-0000-0000-0000-000000000000",
                UserId: "00000000-0000-0000-0000-000000000000",
                ReceiverName: $('input[name="ReceiverName"]').val(),
                Email: $('input[name="ReceiverEmail"]').val(),
                Phone: $('input[name="ReceiverPhone"]').val(),
                CityId: $('select[name="ReceiverCityId"]').val(),
                Address: $('input[name="ReceiverAddress"]').val(),
                Contact: $('input[name="ReceiverContact"]').val(),
                PostalCode: $('input[name="ReceiverPostalCode"]').val(),
                OtherAddress: $('input[name="ReceiverOtherAddress"]').val()
            },

            ShipingTypeId: $('select[name="ShipingTypeId"]').val(),
            ShipingPackgingId: $('select[name="ShipingPackgingId"]').val(),

            Width: parseFloat($('input[name="Width"]').val()) || 0,
            Height: parseFloat($('input[name="Height"]').val()) || 0,
            Weight: parseFloat($('input[name="Weight"]').val()) || 0,
            Length: parseFloat($('input[name="Length"]').val()) || 0,

            PackageValue: parseFloat($('input[name="PackageValue"]').val()) || 0,
            ShippingRate: 0.0,

            PaymentMethodId: null,
            UserSubscriptionId: null,
            TrackingNumber: null,
            ReferenceId: null
        };
        switch (ShipmentService.FormIds.CurrentState) {
            case 2:
                shipmentDto.CarrierId = $('select[name="DeliveryManId"]').val();
                break;
            case 3:
                shipmentDto.DelivryDate = $('input[name="DeliveryDate"]').val();
                break;

        }
        return shipmentDto;
    },

    FillShipmentForm: function (data)
    {
        console.log("CurrentState from API:", data.CurrentState);


        this.FormIds = {
            Id: data.Id,
            SenderId: data.SenderId,
            ReceiverId: data.ReceiverId,
            TrackingNumber: data.TrackingNumber,
            ShipingRate: data.ShipingRate,
            CurrentState: data.CurrentState,
            ShipingTypeId: data.ShipingTypeId,
            ShipingPackgingId: data.ShipingPackgingId,
        };

        // بيانات المرسل: SenderData
        $('input[name="SenderName"]').val(data.SenderData?.SenderName || "");
        $('input[name="Email"]').val(data.SenderData?.Email || "");
        $('input[name="Phone"]').val(data.SenderData?.Phone || "");
        $('select[name="SendercountryId"]').val(data.SenderData?.CountryId || "");
        DropdownHelper.fillCityDropdown('select[name="SenderCityId"]', data.SenderData?.CountryId, data.SenderData?.CityId);
        $('input[name="Address"]').val(data.SenderData?.Address || "");
        $('input[name="Contact"]').val(data.SenderData?.Contact || "");
        $('input[name="PostalCode"]').val(data.SenderData?.PostalCode || "");
        $('input[name="OtherAddress"]').val(data.SenderData?.OtherAddress || "");

        // بيانات المستقبل: ReciverData
        $('input[name="ReceiverName"]').val(data.ReciverData?.ReceiverName || "");
        $('input[name="ReceiverEmail"]').val(data.ReciverData?.Email || "");
        $('input[name="ReceiverPhone"]').val(data.ReciverData?.Phone || "");
        $('select[name="ReciverCountryId"]').val(data.ReciverData?.CountryId || "");
        DropdownHelper.fillCityDropdown('select[name="ReceiverCityId"]', data.ReciverData?.CountryId, data.ReciverData?.CityId);
        $('input[name="ReceiverAddress"]').val(data.ReciverData?.Address || ""); 
        $('input[name="ReceiverContact"]').val(data.ReciverData?.Contact || "");
        $('input[name="ReceiverPostalCode"]').val(data.ReciverData?.PostalCode || "");
        $('input[name="ReceiverOtherAddress"]').val(data.ReciverData?.OtherAddress || "");

        // تفاصيل الشحنة
        $('select[name="ShipingTypeId"]').val(data.ShipingTypeId || "");
        $('select[name="ShipingPackgingId"]').val(data.ShipingPackgingId || "");
        $('input[name="Width"]').val(data.Width);
        $('input[name="Height"]').val(data.Height);
        $('input[name="Weight"]').val(data.Weight);
        $('input[name="Length"]').val(data.Length);
        $('input[name="PackageValue"]').val(data.PackageValue);
        $('input[name="TrackingNumber"]').val(data.TrackingNumber ?? "");

        $('input[name="ShipingDate"]').val(new Date(data.ShipingDate).toISOString().split("T")[0]);
        $('input[name="DelivryDate"]').val(new Date(data.DelivryDate).toISOString().split("T")[0]);
        switch (data.CurrentState) {
            case 1:
                $("#mainButton").val("Approve");
                break;
            case 2:
                $("#mainButton").val("ReadyForShip");
                $("#deliveryManWrapper").show();
                break;
            case 3:
                $("#mainButton").val("Shipped");
                $("#deliveryDateWrapper").show();
                break;
            case 4:
                $("#mainButton").val("Deliverd");
                $("#secandryButton").show("Returned");
                break;
                
        }

    },

    SaveShippment: function () {
        let data = ShipmentService.GetModel();
        console.log("log data before send");
        console.log(data);
        ApiClient.post("/api/Shipments/Create", data,
            function (data) { }, function (xhr) {
                console.error("API Error:", xhr.responseJSON);
            });
    },
    EditShippment: function () {
        let data = ShipmentService.GetModel();
        data.Id = this.FormIds.Id;
        data.SenderId = this.FormIds.SenderId;
        data.ReceiverId = this.FormIds.ReceiverId;
        data.TrackingNumber = this.FormIds.TrackingNumber;
        data.ShipingRate = this.FormIds.ShipingRate;
        console.log("log data before send");
        console.log(data);
        console.log("Approve Id:", data.Id);
        ApiClient.post("/api/Shipments/Edit", data,
            function (data) { }, function (xhr) {
                console.error("API Error:", xhr.responseJSON);
            });
    },

    ChangeStatus: function (newStatus)
    {
        let data = ShipmentService.GetModel();
        data.Id = this.FormIds.Id;
        data.SenderId = this.FormIds.SenderId;
        data.ReceiverId = this.FormIds.ReceiverId;
        data.TrackingNumber = this.FormIds.TrackingNumber;
        data.ShipingRate = this.FormIds.ShipingRate;
        data.CurrentState = newStatus;

        ApiClient.post("/api/Shipments/ChangeStatus", data,
            function (data) { }, function (xhr) {
                console.error("API Error:", xhr.responseJSON);
            });
    },





    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/Shipments/${id}`, onSuccess, onError, true);
    },
}
