using BL.Dtos;
using BL.Services.Interfaces.IShipment;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService;

public class TrackingNumberCreatorService : ITrackingNumberCreatorService
{
    public double GenerateTrackingNumber(ShipmentDto Dto)
    {
        return 808599468;
    }
}
