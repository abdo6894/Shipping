using System;
using System.Collections.Generic;

namespace Domains;

public partial class SubscriptionPackage : BaseEntity
{
    public string PackageName { get; set; } = null!;

    public int ShippimentCount { get; set; }

    public double NumberOfKiloMeters { get; set; }

    public double TotalWeight { get; set; }

    public virtual ICollection<UserSubscription> TbUserSubscriptions { get; set; } = new List<UserSubscription>();
}
