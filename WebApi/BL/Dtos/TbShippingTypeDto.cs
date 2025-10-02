using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbShippingTypeDto : BaseDto
    {

        public string? ShippingTypeAname { get; set; }

        public string? ShippingTypeEname { get; set; }

        public double ShippingFactor { get; set; }
    }
}
