using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces.IShipment;

public interface ICalculateRateService 
{
    public decimal CalculateRate(ShipmentDto shipment);
}
