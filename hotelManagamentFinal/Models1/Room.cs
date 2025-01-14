using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public RoomType? RoomType { get; set; }
        public int? RoomTypeId { get; set; } //foreign key
        public int? room_number { get; set; }
        public string? room_floor {  get; set; }
    }
}

