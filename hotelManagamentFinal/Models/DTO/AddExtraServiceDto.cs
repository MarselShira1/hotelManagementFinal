using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO
{
    public class AddExtraServiceDto
    {
        [Required(ErrorMessage = "BookingId is required.")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "ExtraServiceId is required.")]
        public int ExtraServiceId { get; set; }



        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}
