using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippingTypeService.cs
public class ShippingTypeService : GenericService<TbShippingType, TbShippingTypeDto>, IShippingTypeService
{
    public ShippingTypeService(IGenericRepository<TbShippingType> repository, IMappingService mapper)
        : base(repository, mapper) { }
}

