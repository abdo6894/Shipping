using BL.Dtos;
using BL.Dtos.BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class AccountController : Controller
    {
        
       private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserDto user)
        {
            var result = await _userService.LoginAsync(user);
            if (result.Success)
                return RedirectToRoute(new { area = "admin", controller = "Home", action = "Index" });
            else
                return View();
        }

        [Authorize]

        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}

