using BL.Dtos;
using BL.Dtos.BL.Dtos;
using Domains;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.Generic
{
    public interface IUserService
    {
        Task<UserResultDto> RegisterAsync(UserDto registerDto);
        Task<UserResultDto> LoginAsync(UserDto loginDto);
        Task LogoutAsync();
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Guid GetLoggedInUser();
    }

}

