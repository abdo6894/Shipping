using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// CityService.cs
public class CityService : GenericService<TbCity, TbCityDto>, ICityService
{
    public CityService(IGenericRepository<TbCity> repository, IMappingService mapper)
        : base(repository, mapper) { }
}

