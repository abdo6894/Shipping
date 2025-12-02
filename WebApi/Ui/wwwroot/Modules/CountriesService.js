const CountriesService = {
    GetAll: function (onSuccess, onError) {
        ApiClient.get('/api/Countries', onSuccess, onError, false);
    },

    GetById: function (id, onSuccess, onError) {
        ApiClient.get(`/api/Countries/${id}`, onSuccess, onError, false);
    }
};