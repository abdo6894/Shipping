using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos.Payment
{
    public class MarkPaidDto
    {
        public Guid Id { get; set; }
        public string? PaymentGateway { get; set; }
        public string? PaymentReference { get; set; }
    }
}
