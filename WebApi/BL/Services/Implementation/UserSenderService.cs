using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserSenderService.cs
public class UserSenderService : GenericService<TbUserSender, TbUserSenderDto>, IUserSenderService
{
    public UserSenderService(IGenericRepository<TbUserSender> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

