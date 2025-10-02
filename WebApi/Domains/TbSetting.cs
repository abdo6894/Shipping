using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbSetting : BaseEntity
{
    public double? KiloMeterRate { get; set; }

    public double? KilooGramRate { get; set; }
}
