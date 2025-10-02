using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Mapping
{
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<object, TDestination>(source);
        }

        public List<TDestination> MapList<TDestination>(IEnumerable<object> source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }
    }

}
