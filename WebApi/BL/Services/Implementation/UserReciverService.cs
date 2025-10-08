using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserReciverService.cs
public class UserReciverService : GenericService<TbUserReciver, TbUserReciverDto>, IUserReciverService
{
    public UserReciverService(IGenericRepository<TbUserReciver> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

