using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
using System.Threading.Tasks;
// CityService.cs
public class CityService : GenericService<City, CityDto>, ICityService
{
    IGenericVwRepository<VwCitiy> _genericVwRepository;
    IMappingService _mapper;
    public CityService(IGenericRepository<City> repository, IMappingService mapper,IUserService userService,IGenericVwRepository<VwCitiy> genericVwRepository)
        : base(repository, mapper,userService)
    {
        _genericVwRepository = genericVwRepository;
        _mapper = mapper;
    }

    public async Task<List<CityDto>> GetAllCities()
    {
       var cities= await _genericVwRepository.GetList(x=>x.CurrentState>0);
        return  _mapper.MapList<VwCitiy, CityDto>(cities); 
    }

    public async Task<List<CityDto>> GetByCountry(Guid countryId)
    {
        var cities = await _genericVwRepository.GetList(x => x.CurrentState > 0 && x.CountryId==countryId);
        return _mapper.MapList<VwCitiy, CityDto>(cities);
    }
}

