using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.IMaxMind_Ip
{

    public interface IUserCountryProvider
    {
        string GetCountryCode();

    }
}