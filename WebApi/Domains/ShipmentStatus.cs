using System;
using System.Collections.Generic;

namespace Domains;

public partial class ShipmentStatus : BaseEntity
{
    public Guid? ShipmentId { get; set; }

    public string? Notes { get; set; }

    public Guid CarrierId { get; set; }
    public virtual Carrier Carrier { get; set; } = null!;

    public virtual Shipment? Shippment { get; set; }
}
