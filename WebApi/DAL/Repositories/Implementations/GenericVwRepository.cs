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
        private readonly ShippingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericVwRepository<T>> _log;
        public GenericVwRepository(ShippingContext context, ILogger<GenericVwRepository<T>> log)
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




    }
}
