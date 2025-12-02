using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Ui.Services;

namespace Ui.Controllers
{
    public class AccountController : Controller
    {
       private readonly GenericApiClient _apiclient;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, GenericApiClient apiclient)
        {
            _userService = userService;
            _apiclient = apiclient;
        }
        public IActionResult Login()
        { 
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
      public async Task<IActionResult> Login(LoginDto user)
      {
            if (!ModelState.IsValid)
                return View(user);
            var result = await _userService.LoginAsync(user);
            if (result.Success)
            {
                LoginApiModel apiResult = await _apiclient.PostAsync<LoginApiModel>("api/Auth/login", user);

                if (apiResult == null)
                {
                    ModelState.AddModelError(string.Empty, "API error: Unable to process login.");
                    return View(user);
                }

                var accessToken = apiResult?.AccessToken.ToString();

                if (string.IsNullOrEmpty(accessToken))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(user);
                }
                Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(15)  
                });
                Response.Cookies.Append("RefreshToken", apiResult?.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddDays(7)  // Adjust token expiry based on your needs
                });
                var dbuser= await _userService.GetByEmailAsync(user.Email);
                if (dbuser.Role.Contains("Admin"))
                    return RedirectToRoute(new { area = "admin", controller = "Home", action = "Index" });
                else
                    return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            else
                return View(user);
      }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var result = await _userService.RegisterAsync(user);
            if (result.Success)
            {

                TempData["SuccessMessage"] = "تم التسجيل بنجاح. يرجى تسجيل الدخول.";
                return RedirectToAction("Login");
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Invalid Register attempt");
                return View(user);
            }
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}


