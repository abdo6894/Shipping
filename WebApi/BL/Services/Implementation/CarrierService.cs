using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation
{
    public class CarrierService
    {
    }
}
public class CarrierService : GenericService<Carrier, CarrierDto>, ICarrierService
{
    public CarrierService(IGenericRepository<Carrier> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

