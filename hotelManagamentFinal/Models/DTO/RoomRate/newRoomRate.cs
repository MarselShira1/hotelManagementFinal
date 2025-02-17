using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO.RoomRate

{
    public class newRoomRate
    {
        public string? Emer { get; set; } = null!;

        public string? RateMultiplier { get; set; }
    }

    public class RoomRateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room Rate Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 1000 characters", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Emer { get; set; } = null!;

        [Required(ErrorMessage = "Multiplier is required")]
        [Range(1.00, 10.00, ErrorMessage = "Multiplier must be between 1 and 10")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Multiplier must be a valid number without symbols.")]
        public decimal RateMultiplier { get; set; }

        [Required(ErrorMessage = "Room Type is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Room Type ID must be a valid number.")]
        public int TipDhomeId { get; set; }
        public byte? Invalidated { get; set; }
    }
}
