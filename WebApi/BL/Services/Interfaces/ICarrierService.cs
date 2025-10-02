using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface ICarrierService : IGenericService<TbCarrier, TbCarrierDto> { }
}
