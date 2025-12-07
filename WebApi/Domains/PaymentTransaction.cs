using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Gateway { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
