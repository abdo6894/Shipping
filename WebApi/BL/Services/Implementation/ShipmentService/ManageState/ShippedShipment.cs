using BL.Dtos;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class ShippedShipment : IShipmentStateHandler
    {
        private readonly IShipmentCommand _shipmentCommand;
        private readonly IShipmentQuery _shipmentQuery;
        private readonly IShipmentStatusService _status;
        private readonly IUserService _userService;

        public ShippedShipment(
            IShipmentCommand shipmentCommand,
            IShipmentQuery shipmentQuery,
            IShipmentStatusService status,
            IUserService userService)
        {
            _shipmentCommand = shipmentCommand;
            _shipmentQuery = shipmentQuery;
            _status = status;
            _userService = userService;
        }

        public ShipmentstatuesEnum TargetState => ShipmentstatuesEnum.Shipped;

        public async Task HandleState(ShipmentDto shipment)
        {

            var current = await _shipmentQuery.GetShipment(shipment.Id);

            if (!current.IsPaid)
            {
                throw new InvalidOperationException("لا يمكن شحن الطلب قبل إتمام الدفع.");
            }

            var userId = _userService.GetLoggedInUser();
            await _shipmentCommand.EditFields(shipment.Id, a =>
            {
                a.DelivryDate = shipment.DelivryDate;
                a.CurrentState = (int)TargetState;
                a.UpdatedBy = userId;
                a.UpdatedDate = DateTime.UtcNow;
            });

            await _shipmentCommand.ChangeStatus(shipment.Id, (int)TargetState);
            await _status.Add(shipment.Id, TargetState);
        }
    }
}
