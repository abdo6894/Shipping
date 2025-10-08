using BL.Dtos;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CountriesController : Controller
    {
       private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public IActionResult Index()
        {
         var data= _countryService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return View(new TbCountryDto()); 

            var data = _countryService.GetById((Guid)Id); 
            if (data == null) return NotFound();

            TempData["MessageTypes"] = null;

            return View(data);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbCountryDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            { 
                if (data.Id == Guid.Empty)
                    _countryService.Add(data);
                else
                    _countryService.Update(data);
                TempData["MessageType"] =(int) MessageTypes.SaveSucess;
            }
            catch
            {
                TempData["MessageType"] =(int) MessageTypes.SaveFailed;

            }

            return RedirectToAction("Index");   

            
        }
        public IActionResult Delete(Guid Id)
        {
            try
            {
                _countryService.ChangeStatus(Id, 0);
                TempData["MessageType"] =(int) MessageTypes.DeleteSuccess;
            }
            catch
            {
                 TempData["MessageType"] = (int)MessageTypes.DeleteFailed;
            }
   
            return RedirectToAction("Index");
        }
    }
}
