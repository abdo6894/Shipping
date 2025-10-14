using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ShipingTypeDto : BaseDto
    {

        public string? ShipingTypeAname { get; set; }

        public string? ShipingTypeEname { get; set; }

        public double ShipingFactor { get; set; }
    }
}
