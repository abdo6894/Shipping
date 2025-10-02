using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // IPaymentMethodService.cs
    public interface IPaymentMethodService : IGenericService<TbPaymentMethod, TbPaymentMethodDto> { }
}
