using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.IShipment;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Ui.Controllers;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Reviwer,Operation,OperationManger")]
    public class ShipmentsController : Controller
    {
        private readonly IShipmentQuery _shipmentQuery;
        private readonly ILogger<ShipmentController> _logger;

        public ShipmentsController(ILogger<ShipmentController> logger, IShipmentQuery shipmentService)
        {
            _logger = logger;
            _shipmentQuery = shipmentService;
        }

        public async Task<IActionResult> List(int Page = 1)
        {
            ShipmentstatuesEnum? status = ShipmentstatuesEnum.Created;

            if (User.IsInRole("Admin"))
                status = null;
          else if (User.IsInRole("Reviwer"))
               status = ShipmentstatuesEnum.Created;
          else if (User.IsInRole("Operation"))
                status = ShipmentstatuesEnum.Approved;
          else if (User.IsInRole("OperationManger"))
                status = ShipmentstatuesEnum.ReadyForShip;

            var shipments = await _shipmentQuery.GetShipments(Page, 3, false, status);
            return View(shipments);

        }
        public IActionResult Edit(Guid? Id)
        {
            return View();
        }
 

    }
}
