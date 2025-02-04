﻿using System;
using System.Collections.Generic;

using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class RoomRate : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal CmimBaze { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public int TipDhomeId { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual ICollection<RoomRateRange> RoomRateRanges { get; set; } = new List<RoomRateRange>();

    public virtual TipDhome TipDhome { get; set; } = null!;
}
