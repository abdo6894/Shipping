using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbUserSubscription
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime SubscriptionDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TbSubscriptionPackage Package { get; set; } = null!;
}
