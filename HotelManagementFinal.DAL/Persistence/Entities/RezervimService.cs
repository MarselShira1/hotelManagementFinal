using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;

public partial class RezervimService : BaseEntity<int>
{
    public int Id { get; set; }

    public int Rezervim { get; set; }

    public int Sherbim { get; set; }

    public int Sasi { get; set; }

    public virtual Rezervim RezervimNavigation { get; set; } = null!;
}
