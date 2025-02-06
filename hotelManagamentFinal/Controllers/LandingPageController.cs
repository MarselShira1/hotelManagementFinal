using System.Diagnostics;
using hotelManagement.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using hotelManagement.DAL.Persistence.Entities;
using System;

namespace HotelManagementISE.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly ILogger<LandingPageController> _logger;
        private readonly IRoomTypeService _roomType;
        private readonly IBookingService _bookingService;

        public LandingPageController(ILogger<LandingPageController> logger, IRoomTypeService roomType , IBookingService bookingService)
        {
            _logger = logger;
            _roomType = roomType;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async  Task<IActionResult> Rooms()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userId = HttpContext.Session.GetInt32("UserId");

            //Console.WriteLine($"Booking Controller - Session UserName: {userName ?? "NULL"}");
            //Console.WriteLine($"Booking Controller - Session UserEmail: {userEmail ?? "NULL"}");


            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userEmail))
            {
                //Console.WriteLine("No session data found. Redirecting to login.");
                //return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = userName;
            ViewBag.UserEmail = userEmail;
            ViewBag.UserId = userId;
            var roomRates = await _bookingService.GetAllRoomRatesAsync();
            ViewBag.RoomRates = roomRates;

            //Console.WriteLine($"DEBUG: Retrieved {roomRates.Count()} room rates.");


            try
            {
                var rooms = await _roomType.GetAllRoomTypesAsync();
                //Console.WriteLine($" SUCCESS: Retrieved {rooms.Count()} rooms.");

                return View(rooms);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($" ERROR: {ex.Message}");
                return Content($"An error occurred: {ex.Message}");
            }

        }
        public IActionResult Reservations()
        {
            return View();
        }

        public IActionResult AddAccomodation(Akomodim akomodimi)
        {

            return View("Index");
        }

        public JsonResult CheckAvailability(DateTime checkinDate, DateTime checkoutDate, int adults, int children)
        {
            var rooms = _roomType.GetAllRoomTypes();
            return Json(rooms);
        }
    }
}
