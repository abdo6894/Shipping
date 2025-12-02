using BL.Dtos;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class ShippedShipment : IShipmentStateHandler
    {
        IShipmentCommand _Shipment;
        IShipmentStatusService _status;
        private readonly IUserService _userService;

        public ShippedShipment(IShipmentCommand Shipment, IShipmentStatusService status, IUserService userService)
        {
            _Shipment = Shipment;
            _status = status;
            _userService = userService;
        }
        public ShipmentstatuesEnum TargetState { get => ShipmentstatuesEnum.Shipped; }

        public async Task HandleState(ShipmentDto shipment)
        {
            var userId = _userService.GetLoggedInUser();
            await _Shipment.EditFields(shipment.Id, a =>
            {
                a.DelivryDate = shipment.DelivryDate;
                a.CurrentState = (int)TargetState;
                a.UpdatedBy = userId;
                a.UpdatedDate = DateTime.UtcNow;
            });
            await _Shipment.ChangeStatus(shipment.Id, (int)TargetState);
            await _status.Add(shipment.Id, TargetState);


        }
    }



}
