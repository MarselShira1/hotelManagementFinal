using Microsoft.AspNetCore.Mvc;
using hotelManagement.BLL.Services;
using hotelManagamentFinal.Models.DTO.RoomType;
using hotelManagamentFinal.Models.DTO;



namespace HotelManagement.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }
        //Marsel 
        //metoda kthen listen me room types
        //13/02/2025
        [HttpGet]
        public IActionResult GetAllRoomTypes()
        {
            var viewModel = new RoomTypeViewModel
            {
                RoomTypes = _roomTypeService.GetAllRoomTypes()
                    .Select(rt => new RoomTypeDTO
                    {
                        Id = rt.Id,
                        Emer = rt.Emer,
                        Siperfaqe = rt.Siperfaqe ?? 0,
                        Pershkrim = rt.Pershkrim,
                        Kapacitet = rt.Kapacitet,
                        Invalidated = rt.Invalidated
                    }).ToList(),
                NewType = new RoomTypeDTO()
            };

            return Ok(viewModel);
        }
        public IActionResult RoomTypeView()
        {
            var roomTypes = _roomTypeService.GetAllRoomTypes();
            var model = new RoomTypeViewModel
            {
                RoomTypes = roomTypes.Select(rt => new RoomTypeDTO
                {
                    Id = rt.Id,
                    Emer = rt.Emer,
                    CmimBaze = rt.CmimBaze,
                    Siperfaqe = rt.Siperfaqe ?? 0,
                    Pershkrim = rt.Pershkrim,
                    Kapacitet = rt.Kapacitet,
                    Invalidated = rt.Invalidated
                }).ToList(),
                NewType = new RoomTypeDTO()
            };

            return View("RoomTypeView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateType(RoomTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "There were errors in your submission. Please check the fields.";


                var roomTypes = _roomTypeService.GetAllRoomTypes();
                var viewModel = new RoomTypeViewModel
                {
                    RoomTypes = roomTypes.Select(rt => new RoomTypeDTO
                    {
                        Id = rt.Id,
                        Emer = rt.Emer,
                        CmimBaze = rt.CmimBaze,
                        Siperfaqe = rt.Siperfaqe ?? 0,
                        Pershkrim = rt.Pershkrim,
                        Kapacitet = rt.Kapacitet,
                        Invalidated = rt.Invalidated
                    }).ToList(),
                    NewType = model.NewType
                };

                return View("RoomTypeView", viewModel);
            }

            _roomTypeService.AddRoomType(new HotelManagementFinal.Domain.Models.CreateRoomType
            {
                Emer = model.NewType.Emer,
                CmimBaze = model.NewType.CmimBaze,
                Siperfaqe = model.NewType.Siperfaqe,
                Pershkrim = model.NewType.Pershkrim,
                Kapacitet = model.NewType.Kapacitet
            });

            TempData["SuccessMessage"] = "Room Type added successfully!";
            return RedirectToAction("RoomTypeView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditType(RoomTypeDTO model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update room type. Please check the fields.";

                var roomTypes = _roomTypeService.GetAllRoomTypes();
                var viewModel = new RoomTypeViewModel
                {
                    RoomTypes = roomTypes.Select(rt => new RoomTypeDTO
                    {
                        Id = rt.Id,
                        Emer = rt.Emer,
                        CmimBaze = rt.CmimBaze,
                        Siperfaqe = rt.Siperfaqe ?? 0,
                        Pershkrim = rt.Pershkrim,
                        Kapacitet = rt.Kapacitet,
                        Invalidated = rt.Invalidated
                    }).ToList(),
                    NewType = new RoomTypeDTO()
                };

                return View("RoomTypeView", viewModel);
            }

            _roomTypeService.UpdateRoomType(new HotelManagementFinal.Domain.Models.CreateRoomType
            {
                Id = model.Id, 
                Emer = model.Emer,
                CmimBaze = model.CmimBaze,
                Siperfaqe = model.Siperfaqe,
                Pershkrim = model.Pershkrim,
                Kapacitet = model.Kapacitet
            });

            TempData["SuccessMessage"] = "Room Type updated successfully!";
            return RedirectToAction("RoomTypeView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteType(int id)
        {
            var roomType = _roomTypeService.GetRoomTypeById(id);
            if (roomType == null)
            {
                TempData["ErrorMessage"] = "Room type not found!";
                return RedirectToAction("RoomTypeView");
            }

            _roomTypeService.SoftDeleteRoomType(id);
            TempData["SuccessMessage"] = "Room type deleted successfully!";
            return RedirectToAction("RoomTypeView");
        }
    }
}