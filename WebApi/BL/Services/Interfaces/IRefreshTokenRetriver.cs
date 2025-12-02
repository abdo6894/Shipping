using BL.Dtos;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contract
{
    public interface IRefreshTokenRetriver
    {
        public Task<RefreshTokenDto> GetByToken(string token);
    }
}
