using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class SubscriptionPackageDto : BaseDto
    {
        public string PackageName { get; set; } = null!;

        public int ShipimentCount { get; set; }

        public double NumberOfKiloMeters { get; set; }

        public double TotalWeight { get; set; }

    }
}
