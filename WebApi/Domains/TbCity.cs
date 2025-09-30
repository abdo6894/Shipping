using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbCity
{
    public Guid Id { get; set; }

    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TbCountry Country { get; set; } = null!;

    public virtual ICollection<TbUserReciver> TbUserReceivers { get; set; } = new List<TbUserReciver>();

    public virtual ICollection<TbUserSender> TbUserSebders { get; set; } = new List<TbUserSender>();
}
