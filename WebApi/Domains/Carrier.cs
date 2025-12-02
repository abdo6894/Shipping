using System;
using System.Collections.Generic;

namespace Domains;

public partial class Carrier : BaseEntity
{
    public string CarrierName { get; set; } = null!;
    public virtual ICollection<Shipment> TbShipments { get; set; } = new List<Shipment>();
}
