const ShippingPackgingService = {
    GetAll: function (onSuccess, onError) {
        ApiClient.get('/api/ShipingPackging', onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/ShipingPackging/${id}`, onSuccess, onError, false);
    }
};