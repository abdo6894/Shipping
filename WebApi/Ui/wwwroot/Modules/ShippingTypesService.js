const ShippingTypesService = {
    GetAll: function (onSuccess, onError) {
        ApiClient.get('/api/ShipingTypes', onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/ShipingTypes/${id}`, onSuccess, onError, false);
    }
};