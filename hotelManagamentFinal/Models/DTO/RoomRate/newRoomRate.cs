using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO.RoomRate
{
    public class newRoomRate
    {
        public string? Emer { get; set; } = null!;

        public string? CmimBaze { get; set; }
    }

    public class RoomRateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Rate Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Emer { get; set; } = null!;
        [Required(ErrorMessage = "Base Price is required")]
        [Range(1, 10000, ErrorMessage = "Base Price must be between 1 and 10,000")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Base Price must be a valid number without symbols.")]
        public decimal CmimBaze { get; set; }
        [Required(ErrorMessage = "Room Type is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Room Type ID must be a valid number.")]
        public int TipDhomeId { get; set; }
        public byte? Invalidated { get; set; }
    }
}
