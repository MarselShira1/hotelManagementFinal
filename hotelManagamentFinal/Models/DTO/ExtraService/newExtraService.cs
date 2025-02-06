using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO.ExtraService
{
    public class newExtraService
    {
        public string? Emer { get; set; } = null!;
        public string? Pershkrim { get; set; }
    }

    public class ExtraServiceDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service Name is required")]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Emer { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Pershkrim { get; set; }

        public byte? Invalidated { get; set; }
    }
}
