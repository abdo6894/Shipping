using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // IUserSubscriptionService.cs
    public interface IUserSubscriptionService : IGenericService<TbUserSubscription, TbUserSubscriptionDto> { }
}
