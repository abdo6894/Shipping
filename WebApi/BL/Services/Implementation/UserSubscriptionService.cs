using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// UserSubscriptionService.cs
public class UserSubscriptionService : GenericService<UserSubscription, UserSubscriptionDto>, IUserSubscriptionService
{
    public UserSubscriptionService(IGenericRepository<UserSubscription> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper,userService) { }
}

