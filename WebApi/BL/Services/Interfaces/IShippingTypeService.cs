using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // IShippingTypeService.cs
    public interface IShippingTypeService : IGenericService<TbShippingType, TbShippingTypeDto> { }
}
