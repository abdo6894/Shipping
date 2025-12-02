$('form.steps').on('submit', function (e) {
    e.preventDefault(); 


    const submitter = e.originalEvent.submitter;
    const buttonvalue = $(submitter).val();
    const buttonname = $(submitter).attr("id");
    switch (ShipmentService.FormIds.CurrentState) {
        case 1:
        case 2:
        case 3:
                 ShipmentService.ChangeStatus(ShipmentService.FormIds.CurrentState+1);
            break;
        case 4:
            if (buttonname === "mainButton") 
                ShipmentService.ChangeStatus(5);
            else 
                ShipmentService.ChangeStatus(6);
            break;
    }

});
