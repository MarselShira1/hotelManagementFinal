using HotelManagement.Models;
using HotelManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hotelManagement.BLL.Services;
using hotelManagement.Domain.Models;
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

        //Marsel
        //metoda per shtimin e nje dhome
        //15/01/2024
        [HttpPost]
        public IActionResult CreateRoomSql(NewRoomDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            roomsService.AddBrand(new CreateRoom
            {
                RoomFloor = model.RoomFloor,
                RoomNumber = model.RoomNumber.ToString(),
                RoomTypeId = model.RoomTypeId,
            });
            return RedirectToAction(nameof(Index));
           
        }

        public async Task<IActionResult> GetRoomList()
        {
            try
            {
                var rooms = await roomsService.GetRoomsAsync();
                return Json(rooms);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
