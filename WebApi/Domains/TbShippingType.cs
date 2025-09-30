using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShippingType
{
    public Guid Id { get; set; }

    public string? ShippingTypeAname { get; set; }

    public string? ShippingTypeEname { get; set; }

    public double ShippingFactor { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
