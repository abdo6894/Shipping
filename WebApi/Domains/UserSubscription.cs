using System;
using System.Collections.Generic;

namespace Domains;

public partial class UserSubscription : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public virtual SubscriptionPackage Package { get; set; } = null!;
}