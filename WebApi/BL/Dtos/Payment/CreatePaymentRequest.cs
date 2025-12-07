using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.Payment
{

        public class CreatePaymentRequest
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public List<CartItem> Items { get; set; } = new();
         public decimal ShippingValue { get; set; }
        public string? RedirectUrl { get; set; }
    }
}
