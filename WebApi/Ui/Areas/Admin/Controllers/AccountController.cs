﻿using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize]

    public class AccountController : Controller
    {
            private readonly SignInManager<ApplicationUser> _signInManager;

            public AccountController(SignInManager<ApplicationUser> signInManager)
            {
                _signInManager = signInManager;
            }
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
    }
    
}
