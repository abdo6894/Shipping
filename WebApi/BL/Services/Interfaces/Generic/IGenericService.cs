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
        Task<TDto> GetById(Guid id);
        Task<List<TDto>> GetAll();
        Task<(bool,Guid)> Add(TDto entity);
        Task<bool> Update(TDto entity);
        Task<bool> ChangeStatus( Guid UserId,int Status=1);
    }
}
