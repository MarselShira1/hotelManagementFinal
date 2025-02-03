using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models.DTO;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using System;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.BLL.Services;

namespace hotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            
            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");

            Console.WriteLine($"Booking Controller - Session UserName: {userName ?? "NULL"}");
            Console.WriteLine($"Booking Controller - Session UserEmail: {userEmail ?? "NULL"}");

            
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userEmail))
            {
                Console.WriteLine("No session data found. Redirecting to login.");
                //return RedirectToAction("Login", "Account");
            }

            ViewBag.UserName = userName;
            ViewBag.UserEmail = userEmail;

            var roomRates = await _bookingService.GetAllRoomRatesAsync();
            ViewBag.RoomRates = roomRates;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] NewBookingDTO bookingDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                Console.WriteLine("Validation Errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }

                return BadRequest(new
                {
                    success = false,
                    errorMessage = "Invalid form data",
                    details = errors
                });
            }

            try
            {
                DateOnly checkInDate = DateOnly.Parse(bookingDto.CheckIn.ToString());
                DateOnly checkOutDate = DateOnly.Parse(bookingDto.CheckOut.ToString());
                var booking = new Rezervim
                {
                    Dhome = bookingDto.RoomId,
                    RoomRate = bookingDto.RoomRateId,
                    User = bookingDto.UserId,
                    CheckIn = checkInDate, 
                    CheckOut = checkOutDate,
                    Cmim = bookingDto.Price,
                    CreatedOn = DateTime.Now,
                    Invalidated = 1
                };

                await _bookingService.AddBookingAsync(booking);

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
                    errorMessage = ex.Message,
                    innerException = ex.InnerException?.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CalculatePrice([FromBody] NewBookingDTO bookingDto)
        {
            try
            {
                var totalPrice = await _bookingService.CalculatePriceAsync(bookingDto.RoomRateId, bookingDto.CheckIn, bookingDto.CheckOut);
                Console.WriteLine(totalPrice);
                return Ok(new { totalPrice });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
