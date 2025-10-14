using BL.Dtos;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ShipingTypesController : Controller
    {
       private readonly IShipingTypeService _shipingTypeService;
        public ShipingTypesController(IShipingTypeService shipingTypeService)
        {
            _shipingTypeService = shipingTypeService;
        }
        public IActionResult Index()
        {
         var data= _shipingTypeService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return View(new ShipingTypeDto()); 

            var data = _shipingTypeService.GetById((Guid)Id); 
            if (data == null) return NotFound();

            TempData["MessageTypes"] = null;

            return View(data);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ShipingTypeDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            { 
                if (data.Id == Guid.Empty)
                    _shipingTypeService.Add(data);
                else
                    _shipingTypeService.Update(data);
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
                _shipingTypeService.ChangeStatus(Id, 0);
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
