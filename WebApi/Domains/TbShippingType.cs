using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShippingType : BaseEntity
{
    public string? ShippingTypeAname { get; set; }

    public string? ShippingTypeEname { get; set; }

    public double ShippingFactor { get; set; }
    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
