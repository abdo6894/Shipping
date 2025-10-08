using DAL.Data.DbContext;
using DAL.Repositories.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Exceptions;

namespace DAL.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _log;
        public GenericRepository(ShippingContext context, ILogger<GenericRepository<T>> log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _log = log;
        }

        public bool Add(T entity)
        {
            try
            {
                entity.CreatedDate = DateTime.UtcNow;
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Adding for entity of type {typeof(T).Name}", _log);

            }
        }

        public bool ChangeStatus(Guid id,Guid UserId, int status = 1)
        {
            try
            {
                var entity= GetByIdTracking(id);
                entity.CurrentState = status;
                entity.UpdatedBy = UserId;
                entity.UpdatedDate = DateTime.UtcNow;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
               throw new DataAccessException(ex, $"Error changing status for entity of type {typeof(T).Name}", _log);

            }
        }

        public bool Delete(Guid Id)
        {
            try
            {
                var entity = GetById(Id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Deleting for entity of type {typeof(T).Name}", _log);
            }
        }

        public List<T> GetAll()
        {
            try
            {

                return _dbSet.Where(x=>x.CurrentState>0).ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting all for entity of type {typeof(T).Name}", _log);

            }
        }

        public T GetById(Guid id)
        {
            try
            {
               return _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id)!;

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting by Id for entity of type {typeof(T).Name}", _log);
            }
        }
        public T GetByIdTracking(Guid id)
        {
            try
            {
                return _dbSet.FirstOrDefault(e => e.Id == id)!;

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting by Id for entity of type {typeof(T).Name}", _log);
            }
        }

        public bool Update(T entity)
        {
 
            try
            {
                var dbData = GetById(entity.Id);
                entity.CreatedDate = dbData.CreatedDate;
                entity.CreatedBy = dbData.CreatedBy;
                entity.UpdatedDate = DateTime.Now;
                entity.CurrentState = dbData.CurrentState;
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Updating for entity of type {typeof(T).Name}", _log);
            }
        }

    }
}
