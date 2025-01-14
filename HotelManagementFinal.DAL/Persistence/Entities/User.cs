using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;


public partial class User : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public string Mbiemer { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();
}
