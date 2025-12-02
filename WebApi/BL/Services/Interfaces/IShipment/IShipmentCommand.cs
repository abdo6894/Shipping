using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.IShipment
{
    public interface IShipmentCommand : IGenericService<Shipment,ShipmentDto>
    {
        Task<bool> Create(ShipmentDto dto);
        Task<bool> Edit(ShipmentDto dto);
        public Task EditFields(Guid id, Action<Shipment> updateAction);

    }
}
