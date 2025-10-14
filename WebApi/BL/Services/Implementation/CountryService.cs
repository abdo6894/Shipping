using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
// CountryService.cs
public class CountryService : GenericService<Country, CountryDto>, ICountryService
{
    public CountryService(IGenericRepository<Country> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) { }
}

