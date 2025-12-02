$(document).ready(function () {

    // تعبئة الـ dropdowns باستخدام الـobject literal
    DropdownHelper.fillCountryDropdown('select[name="SendercountryId"]');
    DropdownHelper.fillCountryDropdown('select[name="ReciverCountryId"]');

    DropdownHelper.fillShippingTypesDropdown('select[name="ShipingTypeId"]');

    DropdownHelper.fillPackagingTypesDropdown('select[name="ShipingPackgingId"]');

    DropdownHelper.fillCarrierDropdown('select[name="DeliveryManId"]');


    $('select[name="SendercountryId"]').on('change', function () {
        const countryId = $(this).val();
        DropdownHelper.fillCityDropdown('select[name="SenderCityId"]', countryId, null);

    });

    $('select[name="ReciverCountryId"]').on('change', function () {
        const countryId = $(this).val();
        DropdownHelper.fillCityDropdown('select[name="ReceiverCityId"]', countryId, null);

    });


});