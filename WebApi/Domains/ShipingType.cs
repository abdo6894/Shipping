using System;
using System.Collections.Generic;

namespace Domains;

public partial class ShipingType : BaseEntity
{
    public string? ShipingTypeAname { get; set; }

    public string? ShipingTypeEname { get; set; }

    public double ShipingFactor { get; set; }
    public virtual ICollection<Shipment> TbShipments { get; set; } = new List<Shipment>();
}
