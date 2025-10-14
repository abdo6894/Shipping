using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // IShippmentStatusService.cs
    public interface IShipmentStatusService : IGenericService<ShipmentStatus, ShipmentStatusDto> { }
}
