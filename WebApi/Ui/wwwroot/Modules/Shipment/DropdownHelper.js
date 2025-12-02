const DropdownHelper = {

    fillCountryDropdown(selectSelector) {
        CountriesService.GetAll(
            function (response) {
                $(selectSelector).empty();
                $(selectSelector).append('<option value="">Select a country</option>');

                response.Data.forEach(function (country) {
                    $(selectSelector).append(
                        `<option value="${country.Id}">${country.CountryAname}</option>`
                    );
                });
                console.log("Countries loaded into", selectSelector);
                $(selectSelector).find("option").each(function () {
                    console.log("  option value:", $(this).val(), "text:", $(this).text());
                });
            },
            function (error) {
                console.error("Error loading countries:", error);
            }
        );
    },
    fillCityDropdown: function (selectSelector, countryId, cityId) {
        if (!countryId) {
            $(selectSelector).empty();
            $(selectSelector).append('<option value="">Select a city</option>');
            return;
        }

        console.log("fillCityDropdown called for", selectSelector, "countryId:", countryId, "cityId:", cityId);

        CititesService.GetByCountryId(countryId, function (response) {
            $(selectSelector).empty();
            $(selectSelector).append('<option value="">Select a city</option>');

            response.Data.forEach(function (city) {
                $(selectSelector).append(
                    `<option value="${city.Id}">${city.CityAname}</option>`
                );
            });

            if (cityId) {
                $(selectSelector).val(cityId);
            }

            console.log("Cities loaded into", selectSelector);
            $(selectSelector).find("option").each(function () {
                console.log("  option value:", $(this).val(), "text:", $(this).text());
            });
            console.log("Selected city after .val:", $(selectSelector).val());

        }, function (error) {
            console.error('Error fetching cities:', error.responseText);
        });
    },


    fillShippingTypesDropdown(selectSelector) {
        ShippingTypesService.GetAll(
            function (response) {
                $(selectSelector).empty();
                $(selectSelector).append('<option value="">Select a Shipping Type</option>');

                response.Data.forEach(function (type) {
                    $(selectSelector).append(
                        `<option value="${type.Id}">${type.ShipingTypeAname}</option>`
                    );
                });
                console.log(response.Data);

            },
            function (error) {
                console.error("Error loading Shipping Types:", error);
            }
        );
    },

    fillPackagingTypesDropdown(selectSelector) {
        ShippingPackgingService.GetAll(
            function (response) {
                $(selectSelector).empty();
                $(selectSelector).append('<option value="">Select a Shipping Packaging Type</option>');

                response.Data.forEach(function (pkg) {
                    $(selectSelector).append(
                        `<option value="${pkg.Id}">${pkg.ShipingPackgingAname}</option>`
                    );
                });
            },
            function (error) {
                console.error("Error loading Shipping Packaging Types:", error);
            }
        );
    },

    fillCarrierDropdown(selectSelector) {
        CarriersService.GetAll(
            function (response) {
                $(selectSelector).empty();
                $(selectSelector).append('<option value="">Select a Carrier </option>');

                response.Data.forEach(function (Carrier) {
                    $(selectSelector).append(
                        `<option value="${Carrier.Id}">${Carrier.CarrierName}</option>`
                    );
                });
                console.log(response.Data);

            },
            function (error) {
                console.error("Error loading Carrier:", error);
            }
        );
    }
};