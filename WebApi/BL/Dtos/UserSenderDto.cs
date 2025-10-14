using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class UserSenderDto : BaseDto
    {

        public string SenderName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string PostalCode { get; set; }
        public string Contact { get; set; } = null!;
        public string OtherAddress { get; set; } = null!;
        public bool IsPress { get; set; }
        public Guid CityId { get; set; }

        public string Address { get; set; } = null!;
    }
}
