using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Action
{
    public int Id { get; set; }

    public string Action1 { get; set; } = null!;

    public string? Pershkrim { get; set; }

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
