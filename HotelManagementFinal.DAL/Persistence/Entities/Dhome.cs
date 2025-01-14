using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class Dhome : BaseEntity<int>
{
    public int Id { get; set; }

    public int TipDhome { get; set; }

    public int? Kat { get; set; }

    public string NumerDhome { get; set; } = null!;

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual TipDhome TipDhomeNavigation { get; set; } = null!;
}
