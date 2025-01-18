using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class TipDhome : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal? Siperfaqe { get; set; }

    public string? Pershkrim { get; set; }

    public int Kapacitet { get; set; }

    public virtual ICollection<Dhome> Dhomes { get; set; } = new List<Dhome>();
}