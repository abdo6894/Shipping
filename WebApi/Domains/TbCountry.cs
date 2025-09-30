using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCountry
{
    public Guid Id { get; set; }

    public string? CountryAname { get; set; }

    public string? CountryEname { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbCity> TbCities { get; set; } = new List<TbCity>();
}
