using BL.Dtos;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces.IShipment
{
    // IShippmentStatusService.cs
    public interface IShipmentStatusService : IGenericService<ShipmentStatus, ShipmentStatusDto>
    {
        public Task<(bool, Guid)> Add(Guid shipmentId, ShipmentstatuesEnum status);

    }
}
