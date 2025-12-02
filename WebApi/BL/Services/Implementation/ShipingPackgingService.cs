using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippingTypeService.cs
public class ShipingPackgingService : GenericService<ShipingPackging, ShipingPackgingDto>, IShipingPackgingTypes
{
    public ShipingPackgingService(IGenericRepository<ShipingPackging> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

