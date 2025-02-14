using hotelManagamentFinal.Models.DTO;
using hotelManagamentFinal.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelManagamentFinal.Models1

{
    public class UserViewModel
    {

        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public UserDto NewUser { get; set; } = new UserDto();

    }
}
