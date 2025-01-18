using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using hotelManagement.BLL.Services;
using HotelManagementFinal.Domain.Models;

namespace hotelManagementFinal.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypeController(IRoomTypeService service)
        {
            roomTypeService = service;
        }

        // Get all room types
        public IActionResult RoomTypeView()
        {
            var roomTypes = roomTypeService.GetAllRoomTypes().ToList();
            return View(roomTypes);
        }

        // Get a specific room type by ID
        public IActionResult EditRoomType(int id)
        {
            var roomType = roomTypeService.GetRoomTypeById(id);
            if (roomType == null)
            {
                return NotFound();
            }
            var model = new CreateRoomType
            {
                Emer = roomType.Emer,
                Cmim = roomType.Cmim,
                Siperfaqe = (decimal)roomType.Siperfaqe,
                Pershkrim = roomType.Pershkrim,
                Kapacitet = roomType.Kapacitet
            };
            return View(model);
        }

        // Edit an existing room type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoomType(int id, CreateRoomType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                roomTypeService.EditRoomType(id, model);
                return RedirectToAction(nameof(RoomTypeView));
            }
            catch
            {
                return View(model);
            }
        }

        // Create a new room type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoomType(CreateRoomType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                roomTypeService.AddRoomType(model);
                return RedirectToAction(nameof(RoomTypeView));
            }
            catch
            {
                return View(model);
            }
        }

        // Delete a room type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoomType(int id)
        {
            try
            {
                roomTypeService.RemoveRoomType(id);
                return RedirectToAction(nameof(RoomTypeView));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
