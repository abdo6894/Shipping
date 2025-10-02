using AutoMapper;
using BL.Mapping;
using DAL.Repositories.Interfaces;
using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.Generic
{
    public class GenericService<T,TDto> : IGenericService<T,TDto>
        where T : BaseEntity
        where TDto : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IMappingService _mapper;

        public GenericService(IGenericRepository<T> repository,IMappingService mapper)
        {
            _repository = repository;
             _mapper = mapper;

        }

        public bool Add(TDto dto, Guid UserId)
        {
            var entity = _mapper.Map<T>(dto);
            entity.CreatedBy = UserId;  

           return  _repository.Add(entity);
           
        }

        public bool ChangeStatus(TDto dto, Guid UserId, int Status = 1)
        {
            var entity = _mapper.Map<T>(dto);

            return _repository.ChangeStatus(entity.Id, Status);
        }

        public List<TDto> GetAll()
        {

            var entites= _repository.GetAll();
            return _mapper.Map<List<TDto>>(entites);
        }

        public TDto GetById(Guid id)
        {
            var entity= _repository.GetById(id);
            return _mapper.Map<TDto>(entity);

        }

        public bool Update(TDto dto, Guid UserId)
        {
            var entity= _mapper.Map<T>(dto);

            entity.UpdatedBy = UserId;

            return _repository.Update(entity);
        }
    }
}