using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class RefreshToken : BaseEntity
    {

        public string Token { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}