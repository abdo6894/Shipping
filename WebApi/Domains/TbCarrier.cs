using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCarrier
{
    public Guid Id { get; set; }

    public string CarrierName { get; set; } = null!;

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbShippmentStatus> TbShippmentStatuses { get; set; } = new List<TbShippmentStatus>();
}
