using System;
using System.Collections.Generic;

namespace Domains;

public partial class Carrier : BaseEntity
{
    public string CarrierName { get; set; } = null!;
    public virtual ICollection<ShipmentStatus> TbShipmentStatuses { get; set; } = new List<ShipmentStatus>();
}
