using System.Collections.Generic;
using HotelManagement.Models.DTO;

namespace hotelManagamentFinal.Models.DTO
{
    public class CalendarViewModel
    {
        public List<NewRoomDTO> Rooms { get; set; }
        public List<NewBookingDTO> Bookings { get; set; }
    }


}