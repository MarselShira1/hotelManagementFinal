using System.Collections.Generic;
using hotelManagamentFinal.Models.DTO.RoomRate;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Models

{
    public class RoomRateViewModel
    {
        public List<RoomRateDTO> Rates { get; set; } = new List<RoomRateDTO>();
        public RoomRateDTO NewRate { get; set; } = new RoomRateDTO();
        public List<SelectListItem> RoomTypes { get; internal set; }
    }
}
