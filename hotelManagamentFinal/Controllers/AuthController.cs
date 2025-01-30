using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using HotelManagementFinal.BLL.Services;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;

namespace hotelManagamentFinal.Controllers
{
    public class AuthController : Controller
    {
        private readonly HotelManagementDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(HotelManagementDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(NewUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = "Invalid form data"
                });
            }

            try
            {
                var existingUser = _authService.emailExist(userDto.email);
                if (existingUser != null)
                {
                    return Json(new
                    {
                        success = false,
                        errorMessage = "Email already exists"
                    });
                }

                var user = new User
                {
                    Emer = userDto.emer,
                    Mbiemer = userDto.mbiemer,
                    Email = userDto.email,
                    Password = userDto.password,
                    Role = 1,
                    CreatedOn = DateTime.Now,
                    Invalidated = 1
                };

                _authService.Register(user);

                var loginDto = new LogInDTO
                {
                    Email = userDto.email,
                    Password = userDto.password
                };

                return Login(loginDto);

                return Json(new
                {
                    success = true,
                    errorMessage = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = ex.Message
                });
            }
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
            public IActionResult Login(LogInDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", loginDto);
            }

            try
            {
                var user1 = _authService.emailExist(loginDto.Email);
                if (user1 != null)
                {
                    var user = _authService.Login(loginDto.Email, loginDto.Password);
               
                    if (user != null)
                    {
                        HttpContext.Session.SetInt32("RoleId", user.Role);
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserName", user.Emer);

                    return Json(
                       new
                       {
                           Success = true,
                           ErrorMessage = "Success Login"
                       });
                    }
                    return Json(
                        new
                        {
                            Success = false,
                            ErrorMessage = "Invalid login"
                        });
                }
                return Json(
                       new
                       {
                           Success = false,
                           ErrorMessage = "There is no user with this email"
                       });
            }
            catch (Exception ex)
            {

                return Json(
                       new
                       {
                           Success = false,
                           ErrorMessage = "An error occurred, Please try again later!"
                       });
            }
        }


    }
}