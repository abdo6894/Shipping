using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces.IShipment
{
    public interface ITrackingNumberCreatorService 
    {
        double GenerateTrackingNumber(ShipmentDto Dto);
    }

}

