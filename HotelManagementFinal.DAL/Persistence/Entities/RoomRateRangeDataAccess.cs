using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace hotelManagement.DAL.Persistence.Entities;

[Table("room_rate_ranges")]
public partial class RoomRateRangeDataAccess : BaseEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("room_rate_id")]
    public int? RoomRateId { get; set; } //foreign key

    [Column("Start_Date")]
    public DateOnly? StartDate { get; set; }

    [Column("end_Date")]
    public DateOnly? EndDate { get; set; }

    [Column("Weekend_Pricing")]
    public decimal? WeekendPricing { get; set; }

    [Column("Holiday_Pricing")]
    public decimal? HolidayPricing { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public byte? Invalidated { get; set; }

    public virtual RoomRate? RoomRate { get; set; } = null!;
}
