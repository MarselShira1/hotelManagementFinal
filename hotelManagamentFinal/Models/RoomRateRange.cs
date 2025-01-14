using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class RoomRateRange
{
    public int Id { get; set; }

    public int RoomRateId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? WeekendPricing { get; set; }

    public decimal? HolidayPricing { get; set; }

    public string? Description { get; set; }

    public virtual RoomRate RoomRate { get; set; } = null!;
}
