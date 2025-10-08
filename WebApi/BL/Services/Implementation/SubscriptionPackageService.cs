using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// SubscriptionPackageService.cs
public class SubscriptionPackageService : GenericService<TbSubscriptionPackage, TbSubscriptionPackageDto>, ISubscriptionPackageService
{
    public SubscriptionPackageService(IGenericRepository<TbSubscriptionPackage> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

