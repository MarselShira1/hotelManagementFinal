using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Data2;

public partial class Action
{
    public int Id { get; set; }

    public string Action1 { get; set; } = null!;

    public string? Pershkrim { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
