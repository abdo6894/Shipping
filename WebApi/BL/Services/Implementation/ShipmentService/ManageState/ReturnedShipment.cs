using BL.Dtos;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class ReturnedShipment : IShipmentStateHandler
    {
        IShipmentCommand _Shipment;
        IShipmentStatusService _status;
        public ReturnedShipment(IShipmentCommand Shipment, IShipmentStatusService status)
        {
            _Shipment = Shipment;
            _status = status;
        }
        public ShipmentstatuesEnum TargetState { get => ShipmentstatuesEnum.Returned; }

        public async Task HandleState(ShipmentDto shipment)
        {
            await _Shipment.ChangeStatus(shipment.Id, (int)TargetState);
            await _status.Add(shipment.Id, TargetState);


        }
    }



}
