using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserSenderService.cs
public class UserSenderService : GenericService<UserSender, UserSenderDto>, IUserSenderService
{
    IUnitOfWork _uow;

    public UserSenderService(IGenericRepository<UserSender> repo, IMappingService mapper, IUserService userService,
         IUnitOfWork uow)
        : base(uow, mapper, userService)
    {
        _uow= uow;
    }
 
}


