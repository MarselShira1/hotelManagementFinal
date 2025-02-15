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
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;
        private readonly IRezervimServiceService _rezervimServiceService;

        public BookingController(IBookingService bookingService, IMailSenderService mailSenderService,IUserService userService , IRezervimServiceService rezervimServiceService)
        {
            _bookingService = bookingService;
            _mailSenderService = mailSenderService;
            _userService = userService;
            _rezervimServiceService = rezervimServiceService;
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
            errors.ForEach(Console.WriteLine);

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


                var userEmail = _userService.GetUserEmailById(bookingDto.UserId);                      
                var emailSubject = "Booking Confirmation";
                var emailBody = $@"
                    <h2>Booking Confirmation</h2>
                    <p>Dear Customer,</p>
                    <p>Your booking has been successfully confirmed.</p>
                    <p><strong>Check-in:</strong> {checkInDate}</p>
                    <p><strong>Check-out:</strong> {checkOutDate}</p>
                    <p><strong>Total Price:</strong> ${bookingDto.Price}</p>
                    <p>Thank you for choosing our hotel!</p>";

                await _mailSenderService.SendEmailAsync(userEmail, emailSubject, emailBody);
            

            return Json(new { success = true, errorMessage = "" });
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

        [HttpPost]
        public IActionResult AddExtraService([FromBody] AddExtraServiceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _rezervimServiceService.AddExtraService(
                    dto.BookingId,
                    dto.ExtraServiceId,
                    dto.Price
                );
                return Ok();
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while adding the extra service: " + ex.Message);
            }
        }
    }
}
