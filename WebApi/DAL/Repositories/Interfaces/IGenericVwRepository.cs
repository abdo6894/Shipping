using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericVwRepository<T> where T : class 
    {
        Task<T?> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<T?> GetOrDefault(Expression<Func<T, bool>> filter);
        Task<List<T>> GetList(Expression<Func<T, bool>> filter);

    }
}
