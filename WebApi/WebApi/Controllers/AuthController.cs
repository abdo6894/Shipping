using BL.Contract;
using BL.Dtos;
using BL.Dtos;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly IRefreshTokenRetriver _RefreshTokenRetriver;
        #endregion

        #region Constructor
        public AuthController(
            IUserService userService,
            TokenService tokenService,
            IRefreshTokenService refreshTokenService,
            IRefreshTokenRetriver RefreshTokenRetriver)
        {
            _userService = userService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _RefreshTokenRetriver= RefreshTokenRetriver;
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
                Role = user.Role   // الرول الحقيقي من الداتابيز (Admin / Reviwer / ...)
            };

            var accessToken =  _tokenService.GenerateAccessToken(userToken);

                var refreshToken = _tokenService.GenerateRefreshToken();
                var refreshTokenDto = new RefreshTokenDto
                {
                    Token = refreshToken,
                    UserId = user.Id.ToString(),
                    ExpiresAt = DateTime.UtcNow.AddDays(7)
                };
                await _refreshTokenService.SaveOrRefreshToken(refreshTokenDto);

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


            [HttpPost("RefreshAccessToken")]
            public async Task<IActionResult> RefreshAccessToken()
            {
                if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Unauthorized("Refresh token is missing.");
                }
                var storedToken = await _RefreshTokenRetriver.GetByToken(refreshToken);
                if (storedToken == null || storedToken.CurrentState == 2 || storedToken.ExpiresAt < DateTime.UtcNow)
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
                    Role = user.Role   
                };


            var newAccessToken = _tokenService.GenerateAccessToken(userToken);
            Response.Cookies.Append("AccessToken", newAccessToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(15)  // Adjust token expiry based on your needs
            });
            return Ok(new
                {
                    AccessToken = newAccessToken,
                });
            }


            [HttpPost("refresh")]
            public async Task<IActionResult> Refresh()
            {
                if (!Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Unauthorized("Refresh token is missing.");
                }
                var storedToken = await _RefreshTokenRetriver.GetByToken(refreshToken);
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
                await _refreshTokenService.SaveOrRefreshToken(refreshTokenDto);
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

