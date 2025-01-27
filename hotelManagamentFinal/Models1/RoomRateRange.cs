using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelManagement.DAL.Persistence.Entities;

namespace HotelManagement.Models
{
    public class RoomRateRange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RoomRate? RoomRate { get; set; }
        public int? rate_id { get; set; } //foreign key
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? weekend_factor { get; set; }
        public decimal? holiday_factor { get; set; }
        public string? note { get; set; }
    }
}