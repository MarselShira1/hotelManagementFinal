using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hotelManagement.BLL.Services;
namespace HotelManagement.Controllers
{
    public class RoomController : Controller 
    {
//
        private readonly HotelManagementDbContext _context;
        private readonly IRoomService roomsService;
        public RoomController(HotelManagementDbContext context, IRoomService service)
        {
            _context = context;
            roomsService = service;
        }
        public IActionResult RoomView()
        {
            var rooms = _context.Rooms.Include(r => r.RoomType).ToList(); 
            var roomTypes = _context.RoomTypes.ToList();
            

            var model = new RoomViewModel
            {
                Room = rooms,
                NewRoom = new Room(),
                RoomTypes = roomTypes ,
                EditRoom = new Room()
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult CreateRoomSql(NewRoomDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            roomsService.AddBrand(new hotelManagement.Domain.Models.CreateRoom
            {
                RoomFloor = model.RoomFloor,
                RoomNumber = model.RoomNumber.ToString(),
                RoomTypeId = model.RoomTypeId,
            });
            return RedirectToAction(nameof(Index));
            if (ModelState.IsValid)
            {
                return Json(new { success = true });
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
        }

                [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoom([FromForm] RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
                    }
                }

                model.RoomTypes = _context.RoomTypes.ToList();
                model.Room = _context.Rooms.Include(r => r.RoomType).ToList();
                return View("RoomView", model);
            }

            var room = new Room
            {
                RoomTypeId = model.NewRoom.RoomTypeId,
                room_floor = model.NewRoom.room_floor,
                room_number = model.NewRoom.room_number
            };

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return RedirectToAction("RoomView");
        }

    }
}
