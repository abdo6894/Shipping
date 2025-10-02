using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbPaymentMethod : BaseEntity
{
    public string? MethdAname { get; set; }

    public string? MethodEname { get; set; }

    public double? Commission { get; set; }

    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
