
namespace hotelManagamentFinal.Models.DTO
{
    public class RoomViewModel
    {
        public Room? NewRoom { get; set; }
        public Room? EditRoom { get; set; }
        public List<Room>? Room { get; set; }
        public List<RoomType1>? RoomTypes { get; set; }

    
    }
}