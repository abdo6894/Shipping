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
    public UserSenderService(IGenericRepository<UserSender> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

