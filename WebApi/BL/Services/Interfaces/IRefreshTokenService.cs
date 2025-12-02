using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // ICountryService.cs
    public interface IRefreshTokenService : IGenericService<RefreshToken, RefreshTokenDto>
    {
        Task<bool> SaveOrRefreshToken(RefreshTokenDto refreshTokenDto);
    }
}
