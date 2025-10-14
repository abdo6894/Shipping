using System;
using System.Collections.Generic;

namespace Domains;

public partial class Country : BaseEntity
{
    public string? CountryAname { get; set; }

    public string? CountryEname { get; set; }

    public virtual ICollection<City> TbCities { get; set; } = new List<City>();
}
