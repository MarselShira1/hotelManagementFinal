using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.Domain.Models;

namespace HotelManagementFinal.Domain.Models
{
    public class RoomRateRange
    {
        public int Id { get; set; }

        public int? RoomRateId { get; set; } //foreign key

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public decimal? WeekendPricing { get; set; }

        public decimal? HolidayPricing { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public byte? Invalidated { get; set; }

        public virtual CreateRoomRate? RoomRate { get; set; } = null!;
    }
}