using BL.Dtos;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces.Generic;
using Domains;
using SharedLiberary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.IShipment
{
    public interface IShipmentQuery : IGenericService<Shipment, ShipmentDto>
    {
        Task<List<ShipmentDto>> GetShipments();
        Task<PageResulet<ShipmentDto>> GetShipments(int pagenumber, int pageSize, bool isuser, ShipmentstatuesEnum? statues);
        Task<ShipmentDto> GetShipment(Guid Id);

    }
}
