using BL.Dtos;
using BL.Services.Implementation.ShipmentService.ManageState;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services;

public interface IShipmentStateHandler
{
    public ShipmentstatuesEnum TargetState {  get; }
    public Task HandleState(ShipmentDto shipment);

}
