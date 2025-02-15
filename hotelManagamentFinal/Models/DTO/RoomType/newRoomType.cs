using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO.RoomType
{
    public class newRoomType
    {
        public string Emer { get; set; } = null!;
        public decimal CmimBaze { get; set; }
        public decimal Siperfaqe { get; set; }
        public string? Pershkrim { get; set; }
        public int Kapacitet { get; set; }
    }
    public class RoomTypeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Type Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Emer { get; set; } = null!;
        public decimal CmimBaze { get; set; }
        [Required(ErrorMessage = "Area is required")]
        [Range(1, 10000, ErrorMessage = "Area must be between 1 and 10,000")]
        public decimal Siperfaqe { get; set; }

        [StringLength( 1000, ErrorMessage = "Description must be between 1 and 1000 characters ")]
        public string? Pershkrim { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 100")]
        public int Kapacitet { get; set; }

        public byte? Invalidated { get; set; } 
    }
}
