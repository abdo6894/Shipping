using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippmentService.cs
public class ShippmentService : GenericService<TbShippment, TbShippmentDto>, IShippmentService
{
    public ShippmentService(IGenericRepository<TbShippment> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

