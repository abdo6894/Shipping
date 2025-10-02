using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Dtos
{
    public class TbSettingDto : BaseDto
    {
        public double? KiloMeterRate { get; set; }

        public double? KilooGramRate { get; set; }
    }
}
