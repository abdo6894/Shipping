using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// PaymentMethodService.cs
public class PaymentMethodService : GenericService<PaymentMethod, PaymentMethodDto>, IPaymentMethodService
{
    public PaymentMethodService(IGenericRepository<PaymentMethod> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

