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
    public class CitiesController : ControllerBase
    {
        ICityService _CitiyService;
        ILogger<CitiesController> _logger;
        public CitiesController(ICityService cityService, ILogger<CitiesController> logger)
        {
            _CitiyService = cityService;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<ApiResponse<CityDto>>>> GetAll()
        {
            try
            {
                var data = await _CitiyService.GetAllCities();
                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(data, "cities types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }
        [HttpGet("GetByCountry/{id}")]

        public async Task<ActionResult<List<ApiResponse<CityDto>>>> GetByCountry(Guid id)
        {
            try
            {
                var data = await _CitiyService.GetByCountry(id);
                return Ok(ApiResponse<List<CityDto>>.SuccessResponse(data, "cities types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500, ApiResponse<List<CityDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CityDto>>> Get(Guid id)
        {
            try
            {
                var data = await _CitiyService.GetById(id);
                return Ok(ApiResponse<CityDto>.SuccessResponse(data, "cities types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<CityDto>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<List<CityDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }



    }
}
