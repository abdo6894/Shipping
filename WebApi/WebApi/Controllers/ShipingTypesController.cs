using BL.Dtos;
using BL.Services.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipingTypesController : ControllerBase
    {
        IShipingTypeService _shippingTypeService;
        ILogger<ShipingTypesController> _logger;
        public ShipingTypesController(IShipingTypeService shippingTypeService, ILogger<ShipingTypesController> logger)
        {
            _shippingTypeService = shippingTypeService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<ApiResponse<ShipingTypeDto>>>> GetAll()
        {
            try
            {
                var data = await _shippingTypeService.GetAll();
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
        public async Task<ActionResult<ApiResponse<ShipingTypeDto>>> Get(Guid id)
        {
            try
            {
                var data = await _shippingTypeService.GetById(id);
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
