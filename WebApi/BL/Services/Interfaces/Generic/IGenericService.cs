using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces.Generic
{
   public  interface IGenericService <T,TDto>
        where T : BaseEntity
       where TDto : class
    {
        TDto GetById(Guid id);
        List<TDto> GetAll();
        bool Add(TDto entity,Guid UserId);
        bool Update(TDto entity, Guid UserId);
        bool ChangeStatus(TDto entity, Guid UserId,int Status=1);
    }
}
