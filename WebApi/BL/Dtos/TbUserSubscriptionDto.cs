using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbUserSubscriptionDto : BaseDto
    {

        public Guid UserId { get; set; }

        public Guid PackageId { get; set; }

        public DateTime SubscriptionDate { get; set; }
    }
}
