using BL.Dtos;
using BL.Services.Interfaces;
using DAL.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        ICountryService _CountryService;
        ILogger<CountriesController> _logger;
        public CountriesController(ICountryService CountryService, ILogger<CountriesController> logger)
        {
            _CountryService = CountryService;
            _logger = logger;
        }
        [HttpGet]
        public ActionResult<List<ApiResponse<CountryDto>>> GetAll()
        {
            try
            {
                var data = _CountryService.GetAll();
                return Ok(ApiResponse<List<CountryDto>>.SuccessResponse(data, "Shipping types retrieved successfully."));
            }
            catch(DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<List<CountryDto>>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<List<CountryDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<CountryDto>> Get(Guid id)
        {
            try
            {
                var data = _CountryService.GetById(id);
                return Ok(ApiResponse<CountryDto>.SuccessResponse(data, "Shipping types retrieved successfully."));
            }
            catch (DataAccessException da)
            {
                _logger.LogError(da, "Data access error in GetAll ShippingTypes");
                return StatusCode(500,ApiResponse<CountryDto>.FailResponse("An error occurred while retrieving shipping types."));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " General error in GetAll ShippingTypes");

                return StatusCode(500,ApiResponse<List<CountryDto>>.FailResponse("An error occurred while retrieving shipping types."));
            }


        }



    }
}
