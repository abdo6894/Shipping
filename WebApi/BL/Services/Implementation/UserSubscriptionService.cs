using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserSubscriptionService.cs
public class UserSubscriptionService : GenericService<TbUserSubscription, TbUserSubscriptionDto>, IUserSubscriptionService
{
    public UserSubscriptionService(IGenericRepository<TbUserSubscription> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper,userService) { }
}

