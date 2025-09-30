using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbShippment
{
    public Guid Id { get; set; }

    public DateTime ShippingDate { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid ShippingTypeId { get; set; }

    public double Width { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public double Length { get; set; }

    public decimal PackageValue { get; set; }

    public decimal ShippingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; }

    public Guid? ReferenceId { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual TbPaymentMethod? PaymentMethod { get; set; }

    public virtual TbUserReciver Receiver { get; set; } = null!;

    public virtual TbUserSender Sender { get; set; } = null!;

    public virtual TbShippingType ShippingType { get; set; } = null!;

    public virtual ICollection<TbShippmentStatus> TbShippmentStatuses { get; set; } = new List<TbShippmentStatus>();
}
