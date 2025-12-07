using BL.Dtos;
using BL.Services.Interfaces;
using BL.Services.Interfaces.IMaxMind_Ip;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;

namespace Ui.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IUserCountryProvider _userCountryProvider;

        public HomeController(IUserCountryProvider userCountryProvider)
        {
            _userCountryProvider = userCountryProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Payment(Guid shipmentId)
        {
            if (shipmentId == Guid.Empty)
            {
                ViewBag.Error = "»Ì«‰«  «·‘Õ‰… €Ì— „ «Õ….";
                return View("Paymob"); 
            }

            var country = _userCountryProvider.GetCountryCode();

            if (country == "EG")
                return RedirectToAction("Paymob", new { shipmentId });
            else
                return RedirectToAction("Checkout", new { shipmentId });
        }


        public IActionResult Paymob(Guid shipmentId)
        {
            if (shipmentId == Guid.Empty)
            {
                ViewBag.Error = "»Ì«‰«  «·‘Õ‰… €Ì— „ «Õ….";
                return View();
            }

            // TODO: Â‰« Â ÃÌ» »Ì«‰«  «·‘Õ‰… „‰ «·‹ API
            return View();
        }

        public IActionResult Checkout(Guid shipmentId)
        {
            if (shipmentId == Guid.Empty)
            {
                ViewBag.Error = "»Ì«‰«  «·‘Õ‰… €Ì— „ «Õ….";
                return View();
            }

            // TODO: ‰›” «·ﬂ·«„ ·Ê Â ” Œœ„ Checkout
            return View();
        }



    }
}
