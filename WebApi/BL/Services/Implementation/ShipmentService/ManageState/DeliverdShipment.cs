using BL.Dtos;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class DeliverdShipment : IShipmentStateHandler
    {
        IShipmentCommand _Shipment;
        IShipmentStatusService _status;
        public DeliverdShipment(IShipmentCommand Shipment, IShipmentStatusService status)
        {
            _Shipment = Shipment;
            _status = status;
        }
        public ShipmentstatuesEnum TargetState { get => ShipmentstatuesEnum.Deliverd; }

        public async Task HandleState(ShipmentDto shipment)
        {
            await _Shipment.ChangeStatus(shipment.Id, (int)TargetState);
            await _status.Add(shipment.Id, TargetState);


        }
    }



}
