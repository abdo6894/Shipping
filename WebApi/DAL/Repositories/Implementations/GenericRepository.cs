using DAL.Data.DbContext;
using DAL.Exceptions;
using DAL.Repositories.Interfaces;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Logging;
using SharedLiberary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ShipingContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _log;
        public GenericRepository(ShipingContext context, ILogger<GenericRepository<T>> log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _log = log;
        }

        public async Task<(bool,Guid)> Add(T entity)
        {
            try
            {
               entity.CreatedDate = DateTime.UtcNow;
              await _dbSet.AddAsync(entity);
              await _context.SaveChangesAsync();
               return (true,entity.Id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Adding for entity of type {typeof(T).Name}", _log);

            }
        }
        public async Task<bool> ChangeStatus(Guid id, Guid UserId, int status = 1)
        {
            try
            {
                var entity = await GetById(id);
                if (entity == null) return false;

                entity.CurrentState = status;
                entity.UpdatedBy = UserId;
                entity.UpdatedDate = DateTime.UtcNow;
               await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error changing status for entity of type {typeof(T).Name}", _log);

            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                var entity = await GetById(Id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Deleting for entity of type {typeof(T).Name}", _log);
            }
        }

        public async Task< List<T>> GetAll()
        {
            try
            {
                return await _dbSet.Where(x => x.CurrentState > 0).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Getting all for entity of type {typeof(T).Name}", _log);

            }
        }

        public async Task<T?> GetById(Guid id)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _log);
            }
        }

        public async Task<T?> GetByIdAsNoTracking(Guid id)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _log);
            }
        }

        public async Task<T?> GetOrDefault(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"", _log);

            }
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "", _log);
            }
        }




        public async Task<bool> Update(T entity)
        {

            try
            {
                var dbData = await GetById(entity.Id);
                entity.CreatedDate = dbData.CreatedDate;
                entity.CreatedBy = dbData.CreatedBy;
                entity.UpdatedDate = DateTime.Now;
                entity.CurrentState = dbData.CurrentState;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Updating for entity of type {typeof(T).Name}", _log);
            }
        }
        public async Task<bool> Update(Guid Id, Action<T> updateAction)
        {
            try
            {
                var entity = await _dbSet.FirstOrDefaultAsync(a => a.Id == Id);
                if (entity == null)
                    return false;

                updateAction(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, $"Error Updating for entity of type {typeof(T).Name}", _log);
            }
        }
        public async Task<List<TResult>> GetList<TResult>(Expression<Func<T, bool>>? filter,
                                                          Expression<Func<T, object>>? orderBy,
                                                          bool isDescending = false,
                                                          params Expression<Func<T, object>>[] includers)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                // Apply includes
                foreach (var include in includers)
                    query = query.Include(include);

                // Apply filter
                if (filter != null)
                    query = query.Where(filter);

                // Apply ordering
                if (orderBy != null)
                    query = isDescending
                        ? query.OrderByDescending(orderBy)
                        : query.OrderBy(orderBy);

                query = query.AsNoTracking();


                return await query.Cast<TResult>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex, "Error while fetching list with includes/order/filter", _log); // Or your custom exception
            }
        }



        public async Task<PageResulet<T>> GetPageResulet(
            int pagenumber, int pagesize,
            Expression<Func<T, bool>>? filter,
            Expression<Func<T, object>>? orderBy,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includers)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            if (includers != null)
            {
                foreach (var include in includers)
                    query = query.Include(include);
            }

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = isDescending
                    ? query.OrderByDescending(orderBy)
                    : query.OrderBy(orderBy);

            var totalCount = await query.CountAsync();

            var data = await query
                .Skip((pagenumber - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return new PageResulet<T>
            {
                PageNumber = pagenumber,
                PageSize = pagesize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pagesize),
                totalCount = totalCount,
                Data = data
            };
        }

    }

}