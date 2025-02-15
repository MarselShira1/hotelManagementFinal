using hotelManagamentFinal.Models.DTO;
using HotelManagementFinal.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace hotelManagamentFinal.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData( DateOnly startDate, DateOnly endDate)
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync(startDate, endDate);

            
            var dto = new DashboardDto
            {
                TotalBookings = dashboardData.TotalBookings,
                TopClients = dashboardData.TopClients.Select(c => new TopClientDto
                {
                    ClientName = c.ClientName,
                    TotalBookings = c.TotalBookings,
                    TotalSpending = c.TotalSpending
                }).ToList(),
                RoomRateStats = dashboardData.RoomRateStats.Select(r => new RoomRateStatsDto
                {
                    RoomRateName = r.RoomRateName,
                    TotalBookings = r.TotalBookings
                }).ToList(),
                RevenueTrend = dashboardData.RevenueTrend.Select(r => new RevenueTrendDto
                {
                    Month = r.Month,
                    TotalRevenue = r.TotalRevenue
                }).ToList()
            };

            return Ok(dto); 
        //    var testData = new
        //    {
        //        totalBookings = 10,
        //        topClients = new List<object>
        //{
        //    new { name = "John Doe", bookings = 5 },
        //    new { name = "Jane Smith", bookings = 3 }
        //},
        //        roomRateStats = new List<object>
        //{
        //    new { roomRate = "Standard", bookings = 7 },
        //    new { roomRate = "Deluxe", bookings = 3 }
        //},
        //        revenueTrend = new List<object>
        //{
        //    new { month = "January", totalRevenue = 1000 },
        //    new { month = "February", totalRevenue = 1200 }
        //}
        //    };

        //    return Ok(testData);
        }

        public IActionResult DashboardView()
        {
            return View();
        }
    }


}
