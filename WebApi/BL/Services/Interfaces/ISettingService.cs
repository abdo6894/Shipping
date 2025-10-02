using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // ISettingService.cs
    public interface ISettingService : IGenericService<TbSetting, TbSettingDto> { }
}
