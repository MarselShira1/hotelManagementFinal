using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.Domain.Models;

namespace HotelManagement.Models
{
    public class RoomRateRangesViewModel
    {
        public RoomRateRange? NewRoomRateRange { get; set; }
        public RoomRateRange? EditRoomRateRange { get; set; }
        public List<RoomRateRange>? RoomRateRanges { get; set; }
        public IEnumerable<RoomRate>? roomRates { get; set; }
    }
}