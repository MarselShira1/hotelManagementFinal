using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementISE.Controllers
{
    public class LandingPageController : Controller
    {
        private readonly ILogger<LandingPageController> _logger;

        public LandingPageController(ILogger<LandingPageController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            return View();
        }
        public IActionResult Reservations()
        {
            return View();
        }

    }
}
