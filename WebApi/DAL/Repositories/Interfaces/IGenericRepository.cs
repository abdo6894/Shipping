using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericRepository <T> where T : class 
    {
        T GetById(Guid id);
        T GetByIdTracking(Guid id);
        List<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(Guid Id);
        bool ChangeStatus(Guid id,Guid UserId, int status=1);
    }
}
