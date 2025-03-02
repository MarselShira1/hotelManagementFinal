﻿using System;
using System.Collections.Generic;

namespace hotelManagement.DAL.Persistence.Entities;

public partial class RoomRate : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal RateMultiplier { get; set; }

    public int TipDhomeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual IEnumerable<RoomRateRangeDataAccess> RoomRateRanges { get; set; } = new List<RoomRateRangeDataAccess>();

    public virtual TipDhome TipDhome { get; set; } = null!;
}
