using BL.Dtos;
using BL.Services.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShippingTypesController : ControllerBase
    {
        IShipingTypeService _shippingTypeService;
        ILogger<ShippingTypesController> _logger;
        public ShippingTypesController(IShipingTypeService shippingTypeService, ILogger<ShippingTypesController> logger)
        {
            _shippingTypeService = shippingTypeService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<List<ApiResponse<ShipingTypeDto>>> GetAll()
        {
            try
            {
                var data = _shippingTypeService.GetAll();
                return Ok(ApiResponse<List<ShipingTypeDto>>.SuccessResponse(data, "Shipping types retrieved successfully."));
            }
            catch(DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<List<ShipingTypeDto>>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<List<ShipingTypeDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<ShipingTypeDto>> Get(Guid id)
        {
            try
            {
                var data = _shippingTypeService.GetById(id);
                return Ok(ApiResponse<ShipingTypeDto>.SuccessResponse(data, "Shipping types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<ShipingTypeDto>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<List<ShipingTypeDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }



    }
}
