using HotelManagementFinal.BLL.Services;
using hotelManagamentFinal.Models1;
using Microsoft.AspNetCore.Mvc;
using hotelManagamentFinal.Models.DTO.RoomRate;
using HotelManagement.Models;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagamentFinal.Controllers
{

   public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;
        }

      
        public async Task<IActionResult> UserView()
        {

            // var users = await _userService.GetAllUsersAsync();
            var users = await _userService.GetAllUsersAsync() ?? new List<User>();

            if (!users.Any())  
            {
                return NotFound("No users found in the database.");
            }


            var model = new UserViewModel
            {
                Users = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    emer = user.Emer,
                    mbiemer = user.Mbiemer,
                    email = user.Email

                }).ToList(),
                
              //  NewUser = new UserDto()
            };
           
            return View(model); 
        }
    }
}
