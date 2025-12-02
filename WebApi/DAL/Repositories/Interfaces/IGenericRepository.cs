using Domains;
using Microsoft.EntityFrameworkCore.Query;
using SharedLiberary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
       Task <T?> GetById(Guid id);
        Task<T?> GetByIdAsNoTracking(Guid id);
        Task<List<T>> GetAll();
        Task<(bool,Guid)> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Update(Guid Id,Action<T> updateAction);

        Task<bool> Delete(Guid Id);
        Task<bool> ChangeStatus(Guid id, Guid UserId, int status = 1);
        Task<T?> GetOrDefault(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter);
        Task<List<TResult>> GetList<TResult>(

           Expression<Func<T, bool>>? filter,
           Expression<Func<T, object>>? orderBy,
           bool isDescending = false,
           params Expression<Func<T, object>>[] includers);



        Task<PageResulet<T>> GetPageResulet(
                   int pagenumber,
                   int pagesize,
          Expression<Func<T, bool>>? filter,
          Expression<Func<T, object>>? orderBy,
          bool isDescending = false,
          params Expression<Func<T, object>>[] includers);


    }
}
