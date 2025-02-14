using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models.DTO;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
using System;
using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.BLL.Services;
using hotelManagementFinal.Models1;

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
                    //Id = r.RoomId,
                    RoomNumber = r.RoomNumber
                }).ToList(),

                Bookings = bookings.Select(b => new NewBookingDTO
                {
                    //Id = b.Id,
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
            public async Task<IActionResult> GetBookings(DateTime start, DateTime end)
            {
                var bookings = await _bookingService.GetAllBookingsAsync();

                var bookingList = bookings.Select(booking => new
                {
                    id = booking.Id,
                    resourceId = booking.Dhome,
                    title = "Booked",
                    start = booking.CheckIn.ToString("yyyy-MM-dd"),
                    end = booking.CheckOut.ToString("yyyy-MM-dd"),
                    color = "#ff5733"
                });

                return Json(bookingList);
            }
        }


    }