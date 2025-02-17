
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models1
{
    public class ProfileViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username cannot be longer than 20 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public List<BookingDTO> Bookings { get; set; }
    }

    public class BookingDTO
    {
        public int Id { get; set; }

        [Required]
        public string RoomType { get; set; }

        [Required(ErrorMessage = "Check-in date is required")]
        public string CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        public string CheckOutDate { get; set; }
    }

    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "New password must be at least 8 characters, Uppercases, Lowercases, Numbers")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
