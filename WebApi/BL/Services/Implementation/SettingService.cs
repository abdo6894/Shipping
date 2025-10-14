using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// SettingService.cs
public class SettingService : GenericService<Setting, SettingDto>, ISettingService
{
    public SettingService(IGenericRepository<Setting> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

