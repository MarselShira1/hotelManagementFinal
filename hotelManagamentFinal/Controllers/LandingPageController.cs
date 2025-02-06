using System.Diagnostics;
using hotelManagement.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using hotelManagement.DAL.Persistence.Entities;

namespace HotelManagementISE.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly ILogger<LandingPageController> _logger;
        private readonly IRoomService _room;

        public LandingPageController(ILogger<LandingPageController> logger, IRoomService room)
        {
            _logger = logger;
            _room = room;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Rooms()
        {
            var rooms =await _room.GetRoomsAsync();
            return View(rooms);
        }
        public IActionResult Reservations()
        {
            return View();
        }

        public IActionResult AddAccomodation(Akomodim akomodimi)
        {

            return View("Index");
        }

        public async Task<JsonResult> CheckAvailability(DateTime checkinDate, DateTime checkoutDate, int adults, int children)
        {
            var rooms = await _room.GetRoomsAsync();
            if(rooms.Count()>0)
            {
                rooms = rooms.Where(r=>r.Capacity>=adults+children);
            }
            return Json(rooms);
        }
    }
}
