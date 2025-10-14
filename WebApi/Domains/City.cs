using System;
using System.Collections.Generic;

namespace Domains;

public partial class City : BaseEntity
{
    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<UserReciver> TbUserReceivers { get; set; } = new List<UserReciver>();

    public virtual ICollection<UserSender> TbUserSenders { get; set; } = new List<UserSender>();
}
