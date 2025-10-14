using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippmentService.cs
public class ShipmentService : GenericService<Shipment, ShipmentDto>, IShipmentService
{
    public ShipmentService(IGenericRepository<Shipment> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

