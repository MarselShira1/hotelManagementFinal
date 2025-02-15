namespace HotelManagement.Models.DTO
{
    public class NewRoomDTO
    {
        //esteri hoqi nullable nga RoomId
        public int RoomId { get; set; }
        public int? RoomTypeId { get; set; }
        public int? RoomFloor { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomTypeName { get; set; }
        public double? AverageRating { get; set; } // Holds the average rating (0-5)
        public int? ReviewCount { get; set; }
        public decimal? BasePrice { get; set; }

    }
}
