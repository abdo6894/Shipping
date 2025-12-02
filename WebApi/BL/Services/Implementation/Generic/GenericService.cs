using AutoMapper;
using BL.Mapping;
using BL.Services.Interfaces.Generic;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Implementation.Generic
{
    public class GenericService<T, TDto> : IGenericService<T, TDto>
        where T : BaseEntity
        where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;
        private readonly IMappingService _mapper;
        private readonly IUserService _userService;

        public GenericService(IGenericRepository<T> repository, IMappingService mapper,IUserService userService)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
        }
        public GenericService(IUnitOfWork unitOfWork, IMappingService mapper, IUserService userService)
        {
            _repository = unitOfWork.Repository<T>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool,Guid)> Add(TDto dto)
        {
            var entity = _mapper.Map<TDto, T>(dto);
            entity.CreatedBy =  _userService.GetLoggedInUser();

            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            entity.CurrentState = 1;

            return await  _repository.Add(entity);
        }

        public async Task<bool> ChangeStatus(Guid Id, int Status = 1)
        {
            return await _repository.ChangeStatus(Id,_userService.GetLoggedInUser(), Status);
        }

        public async Task<List<TDto>> GetAll()
        {
            var entities = await _repository.GetAll();
            return _mapper.MapList<T, TDto>(entities);
        }

        public async Task<TDto> GetById(Guid id)
        {
            var entity =  await _repository.GetById(id);
            if(entity == null)
                return null!;
            return _mapper.Map<T, TDto>(entity);
        }

        public async Task<bool> Update(TDto dto)
        {
            var entity = _mapper.Map<TDto, T>(dto);
            entity.UpdatedBy = _userService.GetLoggedInUser();
            return await _repository.Update(entity);
        }
    }
}
