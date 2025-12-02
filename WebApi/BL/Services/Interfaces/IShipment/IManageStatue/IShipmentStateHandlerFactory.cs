using BL.Services.Implementation.ShipmentService.ManageState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.IShipment.IManageStatue
{
    public interface IShipmentStateHandlerFactory
    {
        IShipmentStateHandler GetHandler(ShipmentstatuesEnum status);
    }
}
