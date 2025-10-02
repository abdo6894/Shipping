using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbShippmentStatusDto : BaseDto
    {
      
        public Guid? ShippmentId { get; set; }

        public string? Notes { get; set; }

        public Guid CarrierId { get; set; }
    }
}
