using BL.Dtos;
using BL.Services.Interfaces;
using BL.Services.Interfaces.IShipment;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Ui.Models;

namespace Ui.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly IShipmentQuery _shipmentQuery;
        private readonly ILogger<ShipmentController> _logger;

        public ShipmentController(ILogger<ShipmentController> logger, IShipmentQuery shipmentService)
        {
            _logger = logger;
            _shipmentQuery = shipmentService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> List(int Page=1)
        {
            var shipments = await _shipmentQuery.GetShipments(Page, 3,true,null);
            return View(shipments);

        }
        public IActionResult Show(Guid id)
        {

            return View();
        }
                
        public IActionResult Edit(Guid id)
        {

            return View();
        }
        public IActionResult Delete(Guid Id)
        {
            _shipmentQuery.ChangeStatus(Id,0);
            return RedirectToAction("List");
        }

    }
}
