using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class ShipingPackging : BaseEntity
    {
        public string? ShipingPackgingAname { get; set; }

        public string? ShipingPackgingEname { get; set; }

        public virtual ICollection<Shipment> TbShipments { get; set; } = new List<Shipment>();
    }
}
