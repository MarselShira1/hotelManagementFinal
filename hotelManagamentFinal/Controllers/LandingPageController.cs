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
        private readonly IRoomTypeService _roomType;

        public LandingPageController(ILogger<LandingPageController> logger, IRoomTypeService roomType)
        {
            _logger = logger;
            _roomType = roomType;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            var rooms = _roomType.GetAllRoomTypes();
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

        public JsonResult CheckAvailability(DateTime checkinDate, DateTime checkoutDate, int adults, int children)
        {
            var rooms = _roomType.GetAllRoomTypes();
            return Json(rooms);
        }
    }
}
