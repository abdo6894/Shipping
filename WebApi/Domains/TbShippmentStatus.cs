using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShippmentStatus : BaseEntity
{
    public Guid? ShippmentId { get; set; }

    public string? Notes { get; set; }

    public Guid CarrierId { get; set; }
    public virtual TbCarrier Carrier { get; set; } = null!;

    public virtual TbShippment? Shippment { get; set; }
}
