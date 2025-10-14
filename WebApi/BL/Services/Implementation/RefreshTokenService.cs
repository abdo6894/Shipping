using AutoMapper;
using BL.Dtos;
using BL.Mapping;
using BL.Services.Implementation.Generic;
using BL.Services.Interfaces;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
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

    public RefreshTokenDto GetByToken(string token)
    {
        var entity = _repository.GetOrDefault(x => x.Token == token);
        return _mapper.Map<RefreshToken,RefreshTokenDto>(entity);
    }

    public bool SaveOrRefreshToken(RefreshTokenDto TokenDto)
    {
        var tokenlist = _repository.GetList(a => a.UserId == TokenDto.UserId && a.CurrentState ==1);
        foreach (var token in tokenlist)
        {
            _repository.ChangeStatus(token.Id, Guid.Parse(TokenDto.UserId), 2);
        }
       var dbtokean=  _mapper.Map<RefreshTokenDto, RefreshToken>(TokenDto);
        _repository.Add(dbtokean);

        return true;
    }
}

