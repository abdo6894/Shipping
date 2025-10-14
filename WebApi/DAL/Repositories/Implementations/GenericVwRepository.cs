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
    public class GenericVwRepository<T> : IGenericVwRepository<T> where T : class

    {
        private readonly ShipingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericVwRepository<T>> _log;
        public GenericVwRepository(ShipingContext context, ILogger<GenericVwRepository<T>> log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _log = log;
        }
        public List<T> GetAll()
        {
            try
            {

                return _dbSet.AsNoTracking().ToList();
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
               return _dbSet.AsNoTracking().FirstOrDefault()!;

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting by Id for entity of type {typeof(T).Name}", _log);
            }
        }

        public T GetOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _dbSet.AsNoTracking().FirstOrDefault(filter)!;

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"", _log);

            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                return _dbSet.AsNoTracking().Where(filter).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error in GetList");
                throw;
            }

        }


    }
}
