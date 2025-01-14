using System;
using System.Collections.Generic;
namespace hotelManagement.DAL.Persistence.Entities;


public partial class RoomRate : BaseEntity<int>
{
    public int Id { get; set; }

    public string Emer { get; set; } = null!;

    public decimal CmimBaze { get; set; }

    public virtual ICollection<Rezervim> Rezervims { get; set; } = new List<Rezervim>();

    public virtual ICollection<RoomRateRange> RoomRateRanges { get; set; } = new List<RoomRateRange>();
}
