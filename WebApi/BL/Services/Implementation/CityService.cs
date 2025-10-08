using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// CityService.cs
public class CityService : GenericService<TbCity, TbCityDto>, ICityService
{
    IGenericVwRepository<VwCitiy> _genericVwRepository;
    IMappingService _mapper;
    public CityService(IGenericRepository<TbCity> repository, IMappingService mapper,IUserService userService,IGenericVwRepository<VwCitiy> genericVwRepository)
        : base(repository, mapper,userService)
    {
        _genericVwRepository = genericVwRepository;
        _mapper = mapper;
    }

    public List<TbCityDto> GetAllCities()
    {
       var cities=  _genericVwRepository.GetAll().Where(x=>x.CurrentState>0).ToList();
        return _mapper.MapList<VwCitiy, TbCityDto>(cities); 
    }
}

