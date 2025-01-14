using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Role
{
    public int Id { get; set; }

    public string EmerRoli { get; set; } = null!;

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
