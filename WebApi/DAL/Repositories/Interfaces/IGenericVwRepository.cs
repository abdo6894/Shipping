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
        T GetById(Guid id);
        List<T> GetAll();

    }
}
