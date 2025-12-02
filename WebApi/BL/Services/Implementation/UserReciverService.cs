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
    IUnitOfWork _uow;

    public UserReciverService(IGenericRepository<UserReciver> repo, IMappingService mapper, IUserService userService, IUnitOfWork uow)
        : base(repo, mapper, userService)
    {
        _uow = uow;
    }
}

