using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippingTypeService.cs
public class ShipingTypeService : GenericService<ShipingType, ShipingTypeDto>, IShipingTypeService
{
    public ShipingTypeService(IGenericRepository<ShipingType> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

