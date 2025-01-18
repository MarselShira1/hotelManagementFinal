using System;
using System.Collections.Generic;

namespace hotelManagamentFinal.Models3;

public partial class RoomRate
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal CmimBaze { get; set; }

    public int TipDhomeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual ICollection<RoomRateRange> RoomRateRanges { get; set; } = new List<RoomRateRange>();

    public virtual TipDhome TipDhome { get; set; } = null!;
}
