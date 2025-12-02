using AutoMapper;
using BL.Contract;
using BL.Dtos;
using BL.Mapping;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation
{
    public class RefreshTokenRetriverService : IRefreshTokenRetriver
    {
        IGenericRepository<RefreshToken> _repo;
        IMappingService _mapper;
        public RefreshTokenRetriverService(IGenericRepository<RefreshToken> repo, IMappingService mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<RefreshTokenDto> GetByToken(string token)
        {
            var entity = await _repo.GetOrDefault(x => x.Token == token);
            return _mapper.Map<RefreshToken, RefreshTokenDto>(entity);
        }
    }
}
