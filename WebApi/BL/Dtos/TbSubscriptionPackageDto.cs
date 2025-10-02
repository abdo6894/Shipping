using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbSubscriptionPackageDto : BaseDto
    {
        public string PackageName { get; set; } = null!;

        public int ShippimentCount { get; set; }

        public double NumberOfKiloMeters { get; set; }

        public double TotalWeight { get; set; }

    }
}
