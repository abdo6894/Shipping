using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbUserSubscription : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public virtual TbSubscriptionPackage Package { get; set; } = null!;
}
