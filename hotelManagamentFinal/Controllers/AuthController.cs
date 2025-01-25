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

        public AuthController(HotelManagementDbContext context ,IAuthService authService)
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
                
                return View("~/Views/Home/Index.cshtml", userDto);
            }

            try
            {
                
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

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                
                ViewBag.Error = ex.Message;
                return View("~/Views/Home/Index.cshtml", userDto);
            }
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Home/Index.cshtml");
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
                
                var user = _authService.Login(loginDto.Email, loginDto.Password);

                if(user != null)
                {
                    HttpContext.Session.SetInt32("RoleId", user.Role);
                    HttpContext.Session.SetString("UserEmail", user.Email);
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                
                ViewBag.Error = ex.Message;
                return View("~/Views/Home/Index.cshtml", loginDto);
            }
        }


    }

}
