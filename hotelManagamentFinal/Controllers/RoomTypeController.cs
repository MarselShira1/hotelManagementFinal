using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using HotelManagement.Models.DTO;
using hotelManagement.BLL.Services;
using HotelManagementFinal.Domain.Models;

namespace hotelManagementFinal.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public IActionResult RoomTypeView()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            var model = new RoomTypeViewModel
            {
                RoomTypes = roomTypes.Select(r => new RoomType
                {
                    Id = r.Id,
                    Emer = r.Emer,
                    Siperfaqe = (decimal)r.Siperfaqe,
                    Pershkrim = r.Pershkrim,
                    Kapacitet = r.Kapacitet
                }).ToList(),
                NewRoomType = new RoomType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoomType(RoomType model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));
                var roomTypes = _roomTypeService.GetAllRoomTypes();
                var viewModel = new RoomTypeViewModel
                {
                    RoomTypes = roomTypes.Select(roomType => new RoomType
                    {
                        Id = roomType.Id,
                        Emer = roomType.Emer,
                        Siperfaqe = (decimal)roomType.Siperfaqe,
                        Pershkrim = roomType.Pershkrim,
                        Kapacitet = roomType.Kapacitet
                    }).ToList(),
                    NewRoomType = model
                };
                return View("RoomTypeView", viewModel);
            }

            _roomTypeService.AddRoomType(new CreateRoomType
            {
                Emer = model.Emer,
                Siperfaqe = model.Siperfaqe,
                Pershkrim = model.Pershkrim,
                Kapacitet = model.Kapacitet
            });

            return RedirectToAction("RoomTypeView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRoomType(int id)
        {
            var roomType = _roomTypeService.GetRoomTypeById(id);
            if (roomType != null)
            {
                _roomTypeService.RemoveRoomType(id);
            }
            return RedirectToAction("RoomTypeView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoomType(RoomType model)
        {
            if (!ModelState.IsValid)
            {
                var roomTypes = _roomTypeService.GetAllRoomTypes();
                var viewModel = new RoomTypeViewModel
                {
                    RoomTypes = roomTypes.Select(roomType => new RoomType
                    {
                        Id = roomType.Id,
                        Emer = roomType.Emer,
                        Siperfaqe = (decimal)roomType.Siperfaqe,
                        Pershkrim = roomType.Pershkrim,
                        Kapacitet = roomType.Kapacitet
                    }).ToList(),
                    NewRoomType = new RoomType()
                };
                return View("RoomTypeView", viewModel);
            }

            _roomTypeService.EditRoomType(model.Id, new CreateRoomType
            {
                Emer = model.Emer,
                Siperfaqe = model.Siperfaqe,
                Pershkrim = model.Pershkrim,
                Kapacitet = model.Kapacitet
            });

            return RedirectToAction("RoomTypeView");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRoomType(int id, RoomType model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));
                var roomTypes = _roomTypeService.GetAllRoomTypes();
                var viewModel = new RoomTypeViewModel
                {
                    RoomTypes = roomTypes.Select(roomType => new RoomType
                    {
                        Id = roomType.Id,
                        Emer = roomType.Emer,
                        Siperfaqe = (decimal)roomType.Siperfaqe,
                        Pershkrim = roomType.Pershkrim,
                        Kapacitet = roomType.Kapacitet
                    }).ToList(),
                    NewRoomType = model
                };
                return View("RoomTypeView", viewModel);
            }

            try
            {
                _roomTypeService.UpdateRoomType(id, new CreateRoomType
                {
                    Emer = model.Emer,
                    Siperfaqe = model.Siperfaqe,
                    Pershkrim = model.Pershkrim,
                    Kapacitet = model.Kapacitet
                });
            }
            catch (Exception ex)
            {
                // Trajtoni gabimin në rast se ndodhin ndonjë problem
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("RoomTypeView");
        }

    }
}
