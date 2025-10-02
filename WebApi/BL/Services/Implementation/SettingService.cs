using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// SettingService.cs
public class SettingService : GenericService<TbSetting, TbSettingDto>, ISettingService
{
    public SettingService(IGenericRepository<TbSetting> repository, IMappingService mapper)
        : base(repository, mapper) { }
}

