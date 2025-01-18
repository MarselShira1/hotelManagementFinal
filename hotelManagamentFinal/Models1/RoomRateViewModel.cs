using System.Collections.Generic;

namespace HotelManagement.Models
{
    public class RoomRateViewModel
    {
        public List<RoomRate> Rates { get; set; } = new List<RoomRate>();
        public RoomRate NewRate { get; set; } = new RoomRate();
    }
}
