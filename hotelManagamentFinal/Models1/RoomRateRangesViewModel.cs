using hotelManagement.DAL.Persistence.Entities;

namespace HotelManagement.Models
{
    public class RoomRateRangesViewModel
    {
        public RoomRateRange? NewRoomRateRange { get; set; }
        public RoomRateRange? EditRoomRateRange { get; set; }
        public List<RoomRateRange>? RoomRateRanges { get; set; }
        public List<RoomRate>? roomRates { get; set; }
    }
}