using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces.Generic;
using BL.Services.Interfaces.IShipment;
using DAL.Repositories.Interfaces;
using Domains;

public class ShipmentStatusService : GenericService<ShipmentStatus, ShipmentStatusDto>, IShipmentStatusService
{
    private readonly IGenericRepository<ShipmentStatus> _repo;
    IUnitOfWork _uow;
    IMappingService _mapper;
    IUserService _userService;
    public ShipmentStatusService(IGenericRepository<ShipmentStatus> repository, IUnitOfWork uow, IMappingService mapper, IUserService userService)
        : base(repository, mapper,userService)
    {
        _uow = uow;
        _repo = repository;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<(bool, Guid)> Add(Guid shipmentId, ShipmentstatuesEnum status)
    {
        ShipmentStatusDto oStatus = new ShipmentStatusDto();
        oStatus.ShipmentId = shipmentId;
        oStatus.CurrentState = (int)status;
        var dbObject = _mapper.Map<ShipmentStatusDto, ShipmentStatus>(oStatus);
        dbObject.CreatedBy = _userService.GetLoggedInUser();
        dbObject.CurrentState = 1;
        return await _repo.Add(dbObject);
    }
}

