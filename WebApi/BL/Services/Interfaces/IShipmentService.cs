using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // IShippmentService.cs
    public interface IShipmentService : IGenericService<Shipment, ShipmentDto> { }
}
