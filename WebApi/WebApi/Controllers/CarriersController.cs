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
    public class CarriersController : ControllerBase
    {
        ICarrierService _carrierService;
        ILogger<CarriersController> _logger;
        public CarriersController(ICarrierService carrierService, ILogger<CarriersController> logger)
        {
            _carrierService = carrierService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<ApiResponse<CarrierDto>>>> GetAll()
        {
            try
            {
                var data = await _carrierService.GetAll();
                return Ok(ApiResponse<List<CarrierDto>>.SuccessResponse(data, "carrier types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll carrier");
                return StatusCode(500, ApiResponse<List<CarrierDto>>.FailResponse("An error occurred while retrieving carrier."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll carrier");

                return StatusCode(500, ApiResponse<List<CarrierDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CarrierDto>>> Get(Guid id)
        {
            try
            {
                var data = await _carrierService.GetById(id);
                return Ok(ApiResponse<CarrierDto>.SuccessResponse(data, "carrier types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll carrier");
                return StatusCode(500,ApiResponse<CarrierDto>.FailResponse("An error occurred while retrieving carrier"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll carrier");

                return StatusCode(500,ApiResponse<List<CarrierDto>>.FailResponse("An error occurred while retrieving carrier."));
            }


        }



    }
}
