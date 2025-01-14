using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;


public partial class Role : BaseEntity<int>
{
    public int Id { get; set; }

    public string EmerRoli { get; set; } = null!;

    public virtual ICollection<Privilegj> Privilegjs { get; set; } = new List<Privilegj>();
}
