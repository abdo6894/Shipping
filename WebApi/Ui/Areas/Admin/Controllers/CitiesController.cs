using BL.Dtos;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CitiesController : Controller
    {
        private readonly ICityService _CityService;
        private readonly ICountryService _CountryService;
        public CitiesController(ICityService cityService, ICountryService countryService)
        {
            _CityService = cityService;
            _CountryService = countryService;
        }
        public IActionResult Index()
        {
            var data = _CityService.GetAllCities();
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            LoadCountries();

            if (Id == null || Id == Guid.Empty)
                return View(new TbCityDto());

            var data = _CityService.GetById((Guid)Id);
            if (data == null) return NotFound();

            TempData["MessageTypes"] = null;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbCityDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
            {
                LoadCountries();
                return View("Edit", data);
            }
            try
            {
                if (data.Id == Guid.Empty)
                    _CityService.Add(data);
                else
                    _CityService.Update(data);
                TempData["MessageType"] = (int)MessageTypes.SaveSucess;
            }
            catch
            {
                TempData["MessageType"] = (int)MessageTypes.SaveFailed;

            }

            return RedirectToAction("Index");


        }
        public IActionResult Delete(Guid Id)
        {
            try
            {
                _CityService.ChangeStatus(Id, 0);
                TempData["MessageType"] = (int)MessageTypes.DeleteSuccess;
            }
            catch
            {
                TempData["MessageType"] = (int)MessageTypes.DeleteFailed;
            }

            return RedirectToAction("Index");
        }
        void LoadCountries()
        {
           var Countries= _CountryService.GetAll();
            ViewBag.Countries = Countries;

        }
    }
}
