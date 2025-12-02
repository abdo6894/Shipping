using BL.Dtos;
using BL.Services.Implementation.ShipmentService.ManageState;
using BL.Services.Interfaces;
using BL.Services.Interfaces.IShipment;
using BL.Services.Interfaces.IShipment.IManageStatue;
using DAL.Exceptions;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLiberary.Common;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ShipmentsController : ControllerBase
    {
        IShipmentStateHandlerFactory _shipmentStateHandlerFactory;
        IShipmentCommand _ShipmentCommand;
        IShipmentQuery _ShipmentQuery;
        ILogger<ShipmentsController> _logger;
        public ShipmentsController(IShipmentCommand ShipmentCommand, IShipmentQuery ShipmentQuery, ILogger<ShipmentsController> logger,
            IShipmentStateHandlerFactory shipmentStateHandlerFactory)
        {
            _ShipmentCommand = ShipmentCommand;
            _ShipmentQuery = ShipmentQuery;
            _logger = logger;
            _shipmentStateHandlerFactory = shipmentStateHandlerFactory;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PageResulet<ShipmentDto>>>>List(int page=1)
        {
    
            try
            {
                bool onlyCurrentUser = true;
                ShipmentstatuesEnum? status = null;

                if (User.IsInRole("Admin"))
                {
                    onlyCurrentUser = false;
                    status = null;
                }
                else if (User.IsInRole("Reviwer"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.Created;
                }
                else if (User.IsInRole("Operation"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.Approved;
                }
                else if (User.IsInRole("OperationManger"))
                {
                    onlyCurrentUser = false;
                    status = ShipmentstatuesEnum.ReadyForShip;
                }
                else
                {
                    onlyCurrentUser = true;
                    status = null;
                }
           

                var data = await _ShipmentQuery.GetShipments(page, 3, onlyCurrentUser, status);

                return Ok(ApiResponse<PageResulet<ShipmentDto>>.SuccessResponse(
                    data, "Shipment retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll Shipment");
                return StatusCode(500,ApiResponse<PageResulet<ShipmentDto>>.FailResponse("An error occurred while retrieving Shipment."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll Shipment");

                return StatusCode(500,ApiResponse<PageResulet<ShipmentDto>>.FailResponse("An error occurred while retrieving Shipment ."));
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ShipmentDto>>> Show(Guid id)
        {
            try
            {
                var data = await _ShipmentQuery.GetShipment(id);
                return Ok(ApiResponse<ShipmentDto>.SuccessResponse(data, "Shipment retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<ShipmentDto>.FailResponse("An error occurred while retrieving Shipment ."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<ShipmentDto>.FailResponse("An error occurred while retrieving Shipment ."));
            }


        }
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ShipmentDto data)
        {
            if (data == null)
            {
                return BadRequest(ApiResponse<string>.FailResponse("Shipment data is required."));
            }

            try
            {
                var result = await _ShipmentCommand.Create(data);

                return Ok(ApiResponse<object>.SuccessResponse(result, "Shipment created successfully."));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return StatusCode(500, ApiResponse<string>.FailResponse("An error occurred while creating the shipment.", errors));
            }
        }
            [HttpPost("Edit")]

       public Task<IActionResult> Edit([FromBody] ShipmentDto data)
            {
                if (data == null)
                {
                    return Task.FromResult<IActionResult>(BadRequest(ApiResponse<string>.FailResponse(" Faild TO Edit")));
                }

                try
                {
                    var result = _ShipmentCommand.Edit(data);

                    return Task.FromResult<IActionResult>(Ok(ApiResponse<object>.SuccessResponse(result, "Shipment Updated successfully.")));
                }
                catch (Exception ex)
                {
                    var errors = new List<string> { ex.Message };
                    return Task.FromResult<IActionResult>(StatusCode(500, ApiResponse<string>.FailResponse("An error occurred while Updated the shipment.", errors)));
                }
            }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ShipmentDto data)
        {
            try
            {
                ShipmentstatuesEnum targetStatus = (ShipmentstatuesEnum)data.CurrentState;

                var result =  _shipmentStateHandlerFactory.GetHandler(targetStatus);
                await result.HandleState(data);

                return Ok(ApiResponse<object>.SuccessResponse("change status successfully."));
            }
            catch (Exception ex)
            {
                var errors = new List<string> { ex.Message };
                return StatusCode(500,
                    ApiResponse<string>.FailResponse("An error occurred while updating the shipment to Shipped.", errors));
            }
        }



    }
}
