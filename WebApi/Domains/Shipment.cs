using System;
using System.Collections.Generic;

namespace Domains;

public partial class Shipment : BaseEntity
{
    public DateTime ShipingDate { get; set; }
    public DateTime DelivryDate { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public Guid ShipingTypeId { get; set; }
    public Guid ShipingPackgingId { get; set; }
    public double Width { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public double Length { get; set; }

    public decimal PackageValue { get; set; }

    public decimal ShipingRate { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public double? TrackingNumber { get; set; }
    public Guid? CarrierId { get; set; }
    public Guid? ReferenceId { get; set; }
    public virtual PaymentMethod? PaymentMethod { get; set; }
    public virtual Carrier Carrier { get; set; } = null!;
    public virtual UserReciver Receiver { get; set; } = null!;

    public virtual UserSender Sender { get; set; } = null!;

    public virtual ShipingType ShipingType { get; set; } = null!;
    public virtual ShipingPackging ShipingPackging { get; set; } = null!;

    public virtual ICollection<ShipmentStatus> TbShipmentStatuses { get; set; } = new List<ShipmentStatus>();
}
