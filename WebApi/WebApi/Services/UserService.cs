using BL.Dtos;
using BL.Dtos;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using Domains;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Services;
namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        TokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor accessor, TokenService tokenService)
        {
            _userManager = userManager;

            _signInManager = signInManager;
            _httpContextAccessor = accessor;
            _tokenService = tokenService;
        }

        public async Task<UserResultDto> RegisterAsync(UserDto registerDto)
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return new UserResultDto { Success = false, Errors = new[] { "Passwords do not match." } };
            }

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Phone = registerDto.Phone
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            var roleResult = await _userManager.AddToRoleAsync(user, registerDto.Role ?? "User");

            if (!roleResult.Succeeded)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = roleResult.Errors?.Select(e => e.Description)
                };
            }

            return new UserResultDto
            {
                Success = result.Succeeded,
                Errors = result.Errors?.Select(e => e.Description)
            };
        }


        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, true, false);

            if (!result.Succeeded)
            {
                return new UserResultDto
                {
                    Success = false,
                    Errors = new[] { "Invalid login attempt." }
                };
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var userDto = new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,

            };
            // افترض أن عندك Inject لـ TokenService
            var accessToken = _tokenService.GenerateAccessToken(userDto);

            return new UserResultDto
            {
                Success = true,
                Token = accessToken
                
            };
        }



        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
        }
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return new UserDto
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users;
            return users.Select(u => new UserDto
            {
                Id = Guid.Parse(u.Id),
                Email = u.Email,

            });
        }

        public Guid GetLoggedInUser()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userId);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }

}
