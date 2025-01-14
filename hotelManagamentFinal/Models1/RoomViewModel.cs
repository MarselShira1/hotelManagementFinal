namespace HotelManagement.Models
{
    public class RoomViewModel
    {
        public Room? NewRoom { get; set; }
        public Room? EditRoom { get; set; }
        public List<Room>? Room { get; set; }
        public List<RoomType>? RoomTypes { get; set; }
    }      
}