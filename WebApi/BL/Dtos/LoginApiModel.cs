using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class LoginApiModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
