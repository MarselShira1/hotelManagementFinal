using System.Collections.Generic;
using hotelManagamentFinal.Models.DTO;
using HotelManagement.Models.DTO;

namespace hotelManagementFinal.Models1
{
    public class CalendarViewModel
    {
        public List<NewRoomDTO> Rooms { get; set; }
        public List<NewBookingDTO> Bookings { get; set; } 
    }

    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
    }
}