using System;
using System.Collections.Generic;

using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagament.DAL.Persistence.Entities;

public partial class RoomRateRange : BaseEntity<int>
{
    public int Id { get; set; }

    public int RoomRateId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? WeekendPricing { get; set; }

    public decimal? HolidayPricing { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual RoomRate RoomRate { get; set; } = null!;
}
