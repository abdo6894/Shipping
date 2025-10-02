using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCity : BaseEntity
{
    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    public virtual TbCountry Country { get; set; } = null!;

    public virtual ICollection<TbUserReciver> TbUserReceivers { get; set; } = new List<TbUserReciver>();

    public virtual ICollection<TbUserSender> TbUserSebders { get; set; } = new List<TbUserSender>();
}
