using BL.Dtos;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;
using Domains;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService.ManageState
{
    public class ApproveShipment : IShipmentStateHandler
    {
        IShipmentCommand _Shipment;
        IShipmentStatusService _status;
        public ApproveShipment(IShipmentCommand Shipment, IShipmentStatusService status)
        {
            _Shipment = Shipment;
            _status = status;
        }
        public ShipmentstatuesEnum TargetState { get => ShipmentstatuesEnum.Approved; }

        public async Task HandleState(ShipmentDto shipment)
        {
           await _Shipment.Edit(shipment);
           await _Shipment.ChangeStatus(shipment.Id, (int)TargetState);
           await _status.Add(shipment.Id, TargetState);


        }
    }



}
