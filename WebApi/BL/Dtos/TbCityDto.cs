using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbCityDto : BaseDto
    {

        public string? CityAname { get; set; }

        public string? CityEname { get; set; }

        public Guid CountryId { get; set; }
        public string? CountryAname { get; set; }

        public string? CountryEname { get; set; }
  
    }
}
