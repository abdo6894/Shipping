using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbPaymentMethodDto : BaseDto
    {
   
        public string? MethdAname { get; set; }

        public string? MethodEname { get; set; }

        public double? Commission { get; set; }

    }
}
