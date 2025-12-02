
using BL.Dtos;
using BL.Services.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipingPackgingController : ControllerBase
    {
        IShipingPackgingTypes _shipingPackgingService;
        ILogger<ShipingPackgingController> _logger;
        public ShipingPackgingController(IShipingPackgingTypes shipingPackgingService, ILogger<ShipingPackgingController> logger)
        {
            _shipingPackgingService = shipingPackgingService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<ApiResponse<ShipingPackgingDto>>>> GetAll()
        {
            try
            {
                var data = await _shipingPackgingService.GetAll();
                return Ok(ApiResponse<List<ShipingPackgingDto>>.SuccessResponse(data, "Shipping Packging retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500, ApiResponse<List<ShipingPackgingDto>>.FailResponse("An error occurred while retrieving shipping Packging."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500, ApiResponse<List<ShipingPackgingDto>>.FailResponse("An error occurred while retrieving shipping Packging."));
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ShipingPackgingDto>>> Get(Guid id)
        {
            try
            {
                var data = await _shipingPackgingService.GetById(id);
                return Ok(ApiResponse<ShipingPackgingDto>.SuccessResponse(data, "Shipping Packging retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500, ApiResponse<ShipingPackgingDto>.FailResponse("An error occurred while retrieving shipping Packging."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500, ApiResponse<List<ShipingPackgingDto>>.FailResponse("An error occurred while retrieving shipping Packging."));
            }


        }
    }
}