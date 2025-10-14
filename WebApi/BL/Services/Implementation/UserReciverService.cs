using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserReciverService.cs
public class UserReciverService : GenericService<UserReciver, UserReciverDto>, IUserReciverService
{
    public UserReciverService(IGenericRepository<UserReciver> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

