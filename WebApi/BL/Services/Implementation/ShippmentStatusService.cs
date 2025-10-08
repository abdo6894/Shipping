using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// ShippmentStatusService.cs
public class ShippmentStatusService : GenericService<TbShippmentStatus, TbShippmentStatusDto>, IShippmentStatusService
{
    public ShippmentStatusService(IGenericRepository<TbShippmentStatus> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper,userService) { }
}

