using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbUserReciver
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string ReceiverName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public int CurrentState { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual TbCity City { get; set; } = null!;

    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
