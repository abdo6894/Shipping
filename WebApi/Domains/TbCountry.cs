using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCountry : BaseEntity
{
    public string? CountryAname { get; set; }

    public string? CountryEname { get; set; }

    public virtual ICollection<TbCity> TbCities { get; set; } = new List<TbCity>();
}
