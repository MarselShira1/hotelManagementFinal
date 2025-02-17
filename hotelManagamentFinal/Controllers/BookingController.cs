using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models.DTO;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using System;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.BLL.Services;
using HotelManagamentFinal.Models;
using Stripe.Checkout;
using Stripe;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using HotelManagementFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using HotelManagement.Models;
using iText.StyledXmlParser.Node;

namespace hotelManagement.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMailSenderService _mailSenderService;
        private readonly IUserService _userService;
        private readonly IRezervimServiceService _rezervimServiceService;
        private readonly StripeSettings _stripeSettings;
        private readonly IPageseService _pageseService;
        private readonly IRoomService _room;

        public BookingController(IBookingService bookingService,IRoomService _roomService, IPageseService pageseService, IOptions<StripeSettings> stripeSettings, IMailSenderService mailSenderService, IUserService userService, IRezervimServiceService rezervimServiceService)
        {
            _bookingService = bookingService;
            _mailSenderService = mailSenderService;
            _userService = userService;
            _rezervimServiceService = rezervimServiceService;
            _stripeSettings = stripeSettings.Value;
            _pageseService = pageseService;
            _room = _roomService;

        }

        //public IActionResult CreateCheckoutSession(string amount)
        //{
        //    var currency = "usd"; // Currency code
        //    var successUrl = Url.Action("Success", "Home", null, Request.Scheme);
        //    var cancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme);
        //    StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

        //    var options = new SessionCreateOptions
        //    {
        //        PaymentMethodTypes = new List<string>
        //{
        //    "card"
        //},
        //        LineItems = new List<SessionLineItemOptions>
        //{
        //    new SessionLineItemOptions
        //    {
        //        PriceData = new SessionLineItemPriceDataOptions
        //        {
        //            Currency = currency,
        //            UnitAmount = Convert.ToInt32(amount) * 100,  // Amount in smallest currency unit
        //            ProductData = new SessionLineItemPriceDataProductDataOptions
        //            {
        //                Name = "Product Name",
        //                Description = "Product Description"
        //            }
        //        },
        //        Quantity = 1
        //    }
        //},
        //        Mode = "payment",
        //        SuccessUrl = successUrl,
        //        CancelUrl = cancelUrl
        //    };

        //    var service = new SessionService();
        //    var session = service.Create(options);

        //    return Redirect(session.Url);
        //}

        public  IActionResult CreateCheckoutSession1([FromBody] NewBookingDTO bookingDto)
        {
            try { 
            var currency = "eur"; // Currency code
            var successUrl = Url.Action("Success", "Booking", new
            {
                sessionId = "{CHECKOUT_SESSION_ID}",
                userId = bookingDto.UserId,
                roomId = bookingDto.RoomId,
                roomRateId = bookingDto.RoomRateId,
                price = bookingDto.Price,
                checkIn = bookingDto.CheckIn.ToString("yyyy-MM-dd"),
                checkOut = bookingDto.CheckOut.ToString("yyyy-MM-dd"),
                totalPrice = bookingDto.totalPrice
            }, Request.Scheme);

            var cancelUrl = Url.Action("Cancel", "Booking", null, Request.Scheme);

                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                // Store necessary data in metadata
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = Convert.ToInt32(bookingDto.Price) * 100, // Amount in smallest currency unit (cents)
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Product Name",
                            Description = "Product Description"
                        }
                    },
                    Quantity = 1
                }
            },
                    Mode = "payment",
                    SuccessUrl = successUrl,
                    CancelUrl = cancelUrl,
                    
                };

                var service = new SessionService();
                var session =   service.Create(options);

                // Return session URL if payment session creation is successful
                return Redirect(session.Url);
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        public IActionResult CreateCheckoutSession(NewBookingDTO bookingDto) // Removed [FromBody]

        {
            var currency = "usd"; // Currency code
            var successUrl = Url.Action("Success", "Booking", new
            {
                sessionId = "{CHECKOUT_SESSION_ID}",
                userId = bookingDto.UserId,
                roomId = bookingDto.RoomId,
                roomRateId = bookingDto.RoomRateId,
                price = bookingDto.Price,
                checkIn = bookingDto.CheckIn.ToString("yyyy-MM-dd"),
                checkOut = bookingDto.CheckOut.ToString("yyyy-MM-dd"),
                totalPrice = bookingDto.totalPrice
            }, Request.Scheme);
            var cancelUrl = Url.Action("Cancel", "Home", null, Request.Scheme);
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = currency,
                    UnitAmount = Convert.ToInt32(bookingDto.Price) * 100,  // Amount in smallest currency unit
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Room Booking",
                        Description = "Booking for your selected room"
                    }
                },
                Quantity = 1
            }
        },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url); // Ensure redirection happens here
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

                var savedBooking = _bookingService.AddBookingAsync(booking);

                var isSavedBill = _bookingService.AddBill(savedBooking.Id);

                PageseModel pageseModel = new PageseModel
                {
                    cmimi = booking.Cmim,
                    fatureId = isSavedBill.Id
                };
                var isSavedPagesa = _pageseService.AddPageseAsync(pageseModel);



                var userEmail = _userService.GetUserEmailById(bookingDto.UserId);
                var userData = _userService.GetUserById(bookingDto.UserId);
                var emailConfirmationDto = new EmailSendingConfirmationDto
                {
                    CheckInDate = bookingDto.CheckIn,
                    CheckOutDate = bookingDto.CheckOut,
                    UserEmail = userEmail,
                    TotalPrice = bookingDto.Price,
                    UserId = bookingDto.UserId
                };
                var fullName = userData.Emer + userData.Mbiemer;
                var emailSubject = "Booking Confirmation";
                var emailBody = $@"
                        <h2>Booking Confirmation</h2>
                        <p>Dear {fullName},</p>
                        <p>Your booking has been successfully confirmed.</p>
                        <p><strong>Check-in:</strong> {checkInDate}</p>
                        <p><strong>Check-out:</strong> {checkOutDate}</p>
                        <p><strong>Total Price:</strong> ${bookingDto.Price}</p>
                        <p>Thank you for choosing our hotel!</p>";

                await _mailSenderService.SendEmailAsync(userEmail, emailSubject, emailBody);

                return RedirectToAction("Rooms", new { success = true });
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

        public async Task<IActionResult> Rooms(bool? success)
        
        {
            //bool paymentSuccess = HttpContext.Session.GetString("PaymentSuccess") == "true";
            //HttpContext.Session.Remove("PaymentSuccess");
            var userName = HttpContext.Session.GetString("UserName");
            var userEmail = HttpContext.Session.GetString("UserEmail");
            var userId = HttpContext.Session.GetInt32("UserId");

            ViewBag.UserName = userName;
            ViewBag.UserEmail = userEmail;
            ViewBag.UserId = userId;
            var roomRates = await _bookingService.GetAllRoomRatesAsync();
            ViewBag.RoomRates = roomRates;
            ViewBag.PaymentSuccess = success;

            try
            {
                var rooms = await _room.GetRoomsAsync();
                //Console.WriteLine($" SUCCESS: Retrieved {rooms.Count()} rooms.");

                return View("LandingPage/Rooms",rooms);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($" ERROR: {ex.Message}");
                return Content($"An error occurred: {ex.Message}");
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


        public async Task<IActionResult> Success(string sessionId, int userId, int roomId, int roomRateId,
            decimal price, DateOnly checkIn, DateOnly checkOut, decimal totalPrice)
        { 
            try
            {
                var bookingConfirmation = new NewBookingDTO
                {
                    UserId = userId,
                    RoomId = roomId,
                    RoomRateId = roomRateId,
                    Price = price,
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    totalPrice = totalPrice
                };

                AddBooking(bookingConfirmation);

                // Initialize Stripe API
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                // Retrieve the Stripe session using the sessionId
                var service = new SessionService();
                var session = await service.GetAsync(sessionId);

                // Check payment status
                if (session.PaymentStatus == "paid")
                {
                    // Retrieve metadata values
                    var checkInDate = session.Metadata["checkIn"];
                    var checkOutDate = session.Metadata["checkOut"];
                    var userEmail = session.Metadata["userEmail"];

                    // Prepare email content
                    var emailSubject = "Booking Confirmation";
                    var emailBody = $@"
                <h2>Booking Confirmation</h2>
                <p>Dear Customer,</p>
                <p>Your booking has been successfully confirmed.</p>
                <p><strong>Check-in:</strong> {checkInDate}</p>
                <p><strong>Check-out:</strong> {checkOutDate}</p>
                <p><strong>Total Price:</strong> ${session.AmountTotal / 100}</p>
                <p>Thank you for choosing our hotel!</p>";

                    // Send confirmation email
                    await _mailSenderService.SendEmailAsync(userEmail, emailSubject, emailBody);

                    // You can set a flag to display success on your view
                    ViewBag.PaymentStatus = "Success";
                }
                else
                {
                    // If payment wasn't successful
                    ViewBag.PaymentStatus = "Failed";
                }
            }
            catch (StripeException stripeEx)
            {
                Console.WriteLine($"Stripe Error: {stripeEx.Message}");
                ViewBag.PaymentStatus = "Failed";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during payment verification: {ex.Message}");
                ViewBag.PaymentStatus = "Failed";
            }

            // Return to the view where you want to display the payment status
            return RedirectToAction("Rooms", "LandingPage", new { success = true });

            // Replace with your actual view name
        }



        public IActionResult Cancel()
        {
            return View("LandingPage/Rooms");
        }

         

        [HttpGet]
        public async Task<IActionResult> GetUserReservations()
        {
            try { 
            var userID = HttpContext.Session.GetInt32("UserId");

            if (userID.HasValue) // Properly checking if userID has a value
            {
                var reservations = await _bookingService.GetUserReservations(userID.Value); // Use userID.Value since it's nullable

                if (reservations == null || !reservations.Any())
                    return NotFound("No reservations found for this user.");

                return Ok(reservations); // Return the list of reservations as JSON
            }

            return Unauthorized("User is not logged in or session has expired."); // Return 401 if no userID is found
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
