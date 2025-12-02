using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IShipment;
using DAL.Repositories.Interfaces;
using Domains;
using SharedLiberary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.ShipmentService;

public class ShipmentQueryService : GenericService<Shipment, ShipmentDto>, IShipmentQuery
{

    IUnitOfWork _uow;
    IUserService _userService;
    IGenericRepository<Shipment> _repo;
    IMappingService _mapper;
    public ShipmentQueryService(IGenericRepository<Shipment> repo, IMappingService mapper,
           IUserService userService,IUnitOfWork uow) : base(uow, mapper, userService)
    {
        _uow = uow;
        _repo = repo;
        _userService = userService;
        _mapper = mapper;

    }

    public async Task<List<ShipmentDto>> GetShipments()
    {
        var userId = _userService.GetLoggedInUser();

        var shipments = await _repo.GetList<Shipment>
    (
       a => a.CreatedBy == userId,
       orderBy: a => a.CreatedDate,
       isDescending: true,
       a => a.Sender,
       a => a.Receiver
   );
        return _mapper.Map<List<Shipment>, List<ShipmentDto>>(shipments);
    }

    public async Task<PageResulet<ShipmentDto>> GetShipments(int pagenumber, int pageSize, bool onlyCurrentUser, ShipmentstatuesEnum? statues)
    {
        int? nstatues = statues.HasValue ? (int)statues.Value : null;

        var userId = _userService.GetLoggedInUser();

        var shipments = await _repo.GetPageResulet(
            pagenumber,
            pageSize,
            a =>
                (!onlyCurrentUser || a.CreatedBy == userId) &&
                (nstatues == null || a.CurrentState == nstatues) &&
                a.CurrentState > 0,
            orderBy: a => a.CreatedDate,
            isDescending: true,
            a => a.Sender,
            a => a.Receiver
        );

        return new PageResulet<ShipmentDto>
        {
            PageNumber = shipments.PageNumber,
            PageSize = shipments.PageSize,
            TotalPages = shipments.TotalPages,
            totalCount = shipments.totalCount,
            Data = _mapper.MapList<Shipment, ShipmentDto>(shipments.Data)
        };
    }

    public async Task<ShipmentDto> GetShipment(Guid id)
    {
        var shipments = await _repo.GetList<Shipment>(
            a => a.Id == id,
            orderBy: a => a.CreatedDate,
            isDescending: true,
            a => a.Sender,
            a => a.Sender.City,
            a => a.Sender.City.Country,
            a => a.Receiver,
            a => a.Receiver.City,
            a => a.Receiver.City.Country
        );

        var shipment = shipments.FirstOrDefault();

        return _mapper.Map<Shipment, ShipmentDto>(shipment!);
    }
}
