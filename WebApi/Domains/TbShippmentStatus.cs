using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShippmentStatus
{
    public Guid Id { get; set; }

    public Guid? ShippmentId { get; set; }

    public int CurrentState { get; set; }

    public string? Notes { get; set; }

    public Guid CarrierId { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TbCarrier Carrier { get; set; } = null!;

    public virtual TbShippment? Shippment { get; set; }
}
