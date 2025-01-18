using System.Collections.Generic;
using hotelManagamentFinal.Models.DTO.RoomRate;

namespace HotelManagement.Models
{
    public class RoomRateViewModel
    {
        public List<RoomRateDTO> Rates { get; set; } = new List<RoomRateDTO>();
        public RoomRateDTO NewRate { get; set; } = new RoomRateDTO();
    }
}
