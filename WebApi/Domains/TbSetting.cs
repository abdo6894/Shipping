using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbSetting
{
    public Guid Id { get; set; }

    public double? KiloMeterRate { get; set; }

    public double? KilooGramRate { get; set; }
}
