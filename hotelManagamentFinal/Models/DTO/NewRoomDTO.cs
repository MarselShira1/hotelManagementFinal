using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DTO
{
    public class NewRoomDTO
    {
 
        public int RoomId { get; set; }
        public int? RoomTypeId { get; set; }
        
        public int? RoomFloor { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTypeName { get; set; }
  
        public decimal? BasePrice { get; set; }

    }
}
