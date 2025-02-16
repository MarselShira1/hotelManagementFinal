using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelManagamentFinal.Models.DTO

{
    public class UserViewModel
    {

        public List<UserDto> Users { get; set; } = new List<UserDto>();
        public UserDto NewUser { get; set; } = new UserDto();

    }
}
