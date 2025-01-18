using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class Role
{
    public int Id { get; set; }

    public string EmerRoli { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
