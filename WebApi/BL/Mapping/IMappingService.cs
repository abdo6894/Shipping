using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public interface IMappingService
    {
        TDestination Map<TDestination>(object source);
        List<TDestination> MapList<TDestination>(IEnumerable<object> source);
    }

}
