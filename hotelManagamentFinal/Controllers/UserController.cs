using HotelManagementFinal.BLL.Services;
using hotelManagamentFinal.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using hotelManagamentFinal.Models.DTO.RoomRate;
using HotelManagement.Models;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using BCrypt.Net;

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
            var users =  _userService.GetRezervationCount() ;



            if (!users.Any())  
            {
                return NotFound("No users found in the database.");
            }


            var model = new UserViewModel
            {
                Users = users.Select(user => new UserDto
                {
                    Id = user.UserId,
                    emer = user.Name,
                    mbiemer = user.Surname,
                    email = user.Email,
                    nrRezervimesh = user.ReservationCount

                }).ToList(),

            };
           
            return View(model); 
        }


        public IActionResult ResetPassword(string oldPass, string newPass)
        {
            var userID = HttpContext.Session.GetInt32("UserId");

            if (!userID.HasValue)
            {
                return Unauthorized("User is not logged in.");
            }

            var user = _userService.GetUserById(userID.Value);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            bool isPasswordUpdated = _userService.UpdatePassword(userID.Value, oldPass, newPass);

            if (!isPasswordUpdated)
            {
                return BadRequest("Failed to update password. Check old password.");
            }

            return Ok("Password updated successfully.");
        }




        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
    }
