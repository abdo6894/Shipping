using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class ShipmentDto : BaseDto
    {
   
        public DateTime ShipingDate { get; set; }
        public DateTime DelivryDate { get; set; }

        public Guid SenderId { get; set; }
        public Guid? CarrierId { get; set; }
        public Guid ReceiverId { get; set; }
        public UserSenderDto SenderData { get; set; }
        public UserReciverDto ReciverData { get; set; }
        public Guid? ShipingTypeId { get; set; }
        public Guid? ShipingPackgingId { get; set; }
        public bool IsPaid { get; set; }
        public string? PaymentGateway { get; set; }
        public string? PaymentReference { get; set; }
        public double Width { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public decimal PackageValue { get; set; }

        public decimal ShipingRate { get; set; }

        public Guid? UserSubscriptionId { get; set; }

        public double? TrackingNumber { get; set; }

        public Guid? ReferenceId { get; set; }
    }
}
