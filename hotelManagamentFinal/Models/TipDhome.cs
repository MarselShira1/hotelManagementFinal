using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class TipDhome
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal Cmim { get; set; }

    public decimal? Siperfaqe { get; set; }

    public string? Pershkrim { get; set; }

    public int Kapacitet { get; set; }

    public virtual ICollection<Dhome> Dhomes { get; set; } = new List<Dhome>();
}
