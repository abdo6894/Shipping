using BL.Dtos;
using BL.Dtos;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly TokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        #endregion

        #region Constructor
        public AuthController(
            IUserService userService,
            TokenService tokenService,
            IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        } 
        #endregion


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.RegisterAsync(registerDto);
            if (result == null)
            {
                return BadRequest("Registration failed.");
            }
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userresulet = await _userService.LoginAsync(loginDto);
            if (userresulet == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var user = await _userService.GetByEmailAsync(loginDto.Email);

            var userToken = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = "User" 
            };

            var accessToken = _tokenService.GenerateAccessToken(userToken);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var refreshTokenDto = new RefreshTokenDto
            {
                Token = refreshToken,
                UserId = user.Id.ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
            _refreshTokenService.SaveOrRefreshToken(refreshTokenDto);

            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = refreshTokenDto.ExpiresAt
            });

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }


        [HttpPost("refresh-access-token")]
        public IActionResult RefreshAccessToken()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }
            var storedToken = _refreshTokenService.GetByToken(refreshToken);
            if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }
            var user = _userService.GetUserByIdAsync(storedToken.UserId).Result;
            if (user == null)
            {
                return Unauthorized("User not found.");
            }
            var userToken = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = "User"
            };

           var newAccessToken = _tokenService.GenerateAccessToken(userToken);
            return Ok(new
            {
                AccessToken = newAccessToken,
            });
        }


        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }
            var storedToken = _refreshTokenService.GetByToken(refreshToken);
            if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var refreshTokenDto = new RefreshTokenDto
            {
                Token = newRefreshToken,
                UserId = storedToken.Id.ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
            _refreshTokenService.SaveOrRefreshToken(refreshTokenDto);
            Response.Cookies.Append("RefreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = refreshTokenDto.ExpiresAt
            });
            return Ok(new
            {
                RefreshToken = newRefreshToken
            });
        }











    }
}

