namespace BL.Dtos.Payment
{
    public class PaymentDto
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public Guid ShipmentId { get; set; }
    }

}
