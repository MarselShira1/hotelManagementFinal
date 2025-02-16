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
        private readonly IRoomService _room;
        //private readonly IRoomTypeService _roomType;
        private readonly IBookingService _bookingService;

        public LandingPageController(ILogger<LandingPageController> logger, IRoomService room, IBookingService bookingService)
        {
            _logger = logger;
            _room = room;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Rooms()
        {

            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userId = HttpContext.Session.GetInt32("UserId");

            ViewBag.UserName = userName;
            ViewBag.UserEmail = userEmail;
            ViewBag.UserId = userId;
            var roomRates = await _bookingService.GetAllRoomRatesAsync();
            ViewBag.RoomRates = roomRates;

            try
            {
                var rooms = await _room.GetRoomsAsync();
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

        public async Task<JsonResult> CheckAvailability(DateOnly checkinDate, DateOnly checkoutDate)
        {
            var rooms = await _bookingService.GetAvailableRooms(checkinDate, checkoutDate);
                return Json(rooms);
        }
    }
}
