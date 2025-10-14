using BL.Dtos;
using BL.Services.Interfaces.Generic;
using Domains;

namespace BL.Services.Interfaces
{
    // ICityService.cs
    public interface ICityService : IGenericService<City, CityDto>
    {
        List<CityDto> GetAllCities();
        List<CityDto> GetByCountry(Guid countryId);
    }
}
