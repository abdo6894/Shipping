using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// PaymentMethodService.cs
public class PaymentMethodService : GenericService<TbPaymentMethod, TbPaymentMethodDto>, IPaymentMethodService
{
    public PaymentMethodService(IGenericRepository<TbPaymentMethod> repository, IMappingService mapper)
        : base(repository, mapper) { }
}

