using System;
using System.Collections.Generic;

namespace Domains;

public partial class PaymentMethod : BaseEntity
{
    public string? MethdAname { get; set; }

    public string? MethodEname { get; set; }

    public double? Commission { get; set; }

    public virtual ICollection<Shipment> TbShipments { get; set; } = new List<Shipment>();
}
