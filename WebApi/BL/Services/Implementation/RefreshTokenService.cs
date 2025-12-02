using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
using System.Threading.Tasks;
// CountryService.cs
public class RefreshTokenService : GenericService<RefreshToken, RefreshTokenDto>, IRefreshTokenService
{
    private readonly IGenericRepository<RefreshToken> _repository;
    private readonly IMappingService _mapper;
    public RefreshTokenService(IGenericRepository<RefreshToken> repository, IMappingService mapper, IUserService userService)
        : base(repository, mapper, userService) 
    {
        _repository = repository;
        _mapper = mapper;

    }



    public async Task<bool> SaveOrRefreshToken(RefreshTokenDto TokenDto)
    {
        var tokenlist = await _repository.GetList(a => a.UserId == TokenDto.UserId && a.CurrentState ==1);
        foreach (var token in tokenlist)
        {
          await  _repository.ChangeStatus(token.Id, Guid.Parse(TokenDto.UserId), 2);
        }
       var dbtokean=  _mapper.Map<RefreshTokenDto, RefreshToken>(TokenDto);
       await _repository.Add(dbtokean);

        return true;
    }
}

