using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models.DTO;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using System;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.BLL.Services;
using hotelManagamentFinal.Models.DTO;

namespace hotelManagementFinal.Controllers
{

    public class CalendarController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IBookingService _bookingService;

        public CalendarController(IRoomService roomService, IBookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> CalendarView()
        {
            var rooms = await _roomService.GetRoomsAsync();
            var bookings = await _bookingService.GetAllBookingsAsync();

            var viewModel = new CalendarViewModel
            {
                Rooms = rooms.Select(r => new NewRoomDTO
                {
                    RoomNumber = r.RoomNumber
                }).ToList(),
                Bookings = bookings.Select(b => new NewBookingDTO
                {
                    RoomId = b.Dhome,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut
                }).ToList()
            };

            return View("CalendarView", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();
            var roomList = rooms.Select(room => new
            {
                id = room.RoomId,
                title = $"Room {room.RoomNumber}"
            });

            return Json(roomList);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] DateOnly start, DateOnly end, int roomId)
        {
            IEnumerable<Rezervim> bookings;

            if (roomId == null)
            {
                bookings = await _bookingService.GetAllBookingsAsync();
            }
            else
            {
                bookings = await _bookingService.GetBookingsByRoomAndDateRangeAsync(roomId, start, end);
            }

            var bookingList = bookings.Select(booking => new
            {
                id = booking.Id,
                title = $"Booking #{booking.Id}",
                start = booking.CheckIn.ToString("yyyy-MM-dd"),
                end = booking.CheckOut.ToString("yyyy-MM-dd"),
                backgroundColor = "#4CAF50",
                borderColor = "#45a049",
                textColor = "#ffffff",
                extendedProps = new
                {
                    roomNumber = booking.Dhome,
                    status = "Confirmed"
                }
            });

            return Json(bookingList);
        }
    }


}