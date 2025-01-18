using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class Action : BaseEntity<int>
{
    public int Id { get; set; }

    public string Action1 { get; set; } = null!;

    public string? Pershkrim { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
