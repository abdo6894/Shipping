using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbPaymentMethod
{
    public Guid Id { get; set; }

    public string? MethdAname { get; set; }

    public string? MethodEname { get; set; }

    public double? Commission { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
