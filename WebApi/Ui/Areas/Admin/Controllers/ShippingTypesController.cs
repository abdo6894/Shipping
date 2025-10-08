using BL.Dtos;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ShippingTypesController : Controller
    {
       private readonly IShippingTypeService _shippingTypeService;
        public ShippingTypesController(IShippingTypeService shippingTypeService)
        {
            _shippingTypeService = shippingTypeService;
        }
        public IActionResult Index()
        {
         var data= _shippingTypeService.GetAll();
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return View(new TbShippingTypeDto()); 

            var data = _shippingTypeService.GetById((Guid)Id); 
            if (data == null) return NotFound();

            TempData["MessageTypes"] = null;

            return View(data);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbShippingTypeDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            { 
                if (data.Id == Guid.Empty)
                    _shippingTypeService.Add(data);
                else
                    _shippingTypeService.Update(data);
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
                _shippingTypeService.ChangeStatus(Id, 0);
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
