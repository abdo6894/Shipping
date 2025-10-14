using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
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

    public List<CityDto> GetAllCities()
    {
       var cities=  _genericVwRepository.GetList(x=>x.CurrentState>0).ToList();
        return _mapper.MapList<VwCitiy, CityDto>(cities); 
    }

    public List<CityDto> GetByCountry(Guid countryId)
    {
        var cities = _genericVwRepository.GetList(x => x.CurrentState > 0 && x.CountryId==countryId).ToList();
        return _mapper.MapList<VwCitiy, CityDto>(cities);
    }
}

