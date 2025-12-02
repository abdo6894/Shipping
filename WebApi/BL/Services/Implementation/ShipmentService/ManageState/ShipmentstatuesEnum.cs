using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService.ManageState;

public enum ShipmentstatuesEnum
{
    deleted= 0,
    Created = 1,
    Approved = 2,
    ReadyForShip = 3,
    Shipped = 4,
    Deliverd = 5,
    Returned = 6,

}
