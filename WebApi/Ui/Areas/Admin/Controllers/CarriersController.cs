using BL.Dtos;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ui.Helpers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CarriersController : Controller
    {
       private readonly ICarrierService _CarrierService;
        public CarriersController(ICarrierService CarrierService)
        {
            _CarrierService = CarrierService;
        }
        public async Task<IActionResult> Index()
        {
         var data= await _CarrierService.GetAll();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? Id)
        {
            if (Id == null || Id == Guid.Empty)
                return View(new CarrierDto()); 

            var data = await _CarrierService.GetById((Guid)Id); 
            if (data == null) return NotFound();

            TempData["MessageTypes"] = null;

            return View(data);    
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CarrierDto data)
        {
            TempData["MessageType"] = null;
            if (!ModelState.IsValid)
                return View("Edit", data);
            try
            { 
                if (data.Id == Guid.Empty)
                 await   _CarrierService.Add(data);
                else
                  await  _CarrierService.Update(data);
                TempData["MessageType"] =(int) MessageTypes.SaveSucess;
            }
            catch
            {
                TempData["MessageType"] =(int) MessageTypes.SaveFailed;

            }

            return RedirectToAction("Index");   

            
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
               await _CarrierService.ChangeStatus(Id, 0);
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
