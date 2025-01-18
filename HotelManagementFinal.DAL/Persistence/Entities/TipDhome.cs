﻿using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class TipDhome
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal? Siperfaqe { get; set; }

    public string? Pershkrim { get; set; }

    public int Kapacitet { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Dhome> Dhomes { get; set; } = new List<Dhome>();

    public virtual ICollection<RoomRate> RoomRates { get; set; } = new List<RoomRate>();
}
