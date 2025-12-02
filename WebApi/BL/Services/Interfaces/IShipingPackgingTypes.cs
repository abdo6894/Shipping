using BL.Dtos;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IShipingPackgingTypes : IGenericService<ShipingPackging, ShipingPackgingDto>
    {

    }
}
