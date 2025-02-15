using hotelManagamentFinal.Models.DTO.RoomRate;
using hotelManagement.BLL.Services;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelManagement.Controllers
{
    public class RoomRateController : Controller
    {
        private readonly IRoomRateService _roomRateService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IRoomService _roomService; 
        private List<SelectListItem> roomTypes;

        public RoomRateController(IRoomRateService roomRateService, IRoomTypeService roomTypeService , IRoomService roomService)
        {
            _roomRateService = roomRateService;
            _roomTypeService = roomTypeService;
            _roomService = roomService;
        }

        public IActionResult RateView()
        {
            var rates = _roomRateService.GetAllRoomRates();
            var roomTypes = _roomRateService.GetAllRoomTypes()
                                     .Select(rt => new SelectListItem
                                     {
                                         Value = rt.Id.ToString(),
                                         Text = rt.Emer
                                     }).ToList();
            var model = new RoomRateViewModel
            {
                Rates = rates.Select(r => new RoomRateDTO
                {
                    Id = r.Id,
                    Emer = r.Emer,
                    RateMultiplier = r.RateMultiplier,
                    TipDhomeId = r.TipDhomeId

                }).ToList(),
                RoomTypes = roomTypes, // per te shtuar select list me tipet e dhomave
                NewRate = new RoomRateDTO()
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRoomRates([FromQuery] int roomId)
        {
            try
            {
                var room = _roomService.GetRoomById(roomId);
                var roomType = _roomTypeService.GetRoomTypeById(room.TipDhome);
                var roomRates =  _roomRateService.GetRoomRatesByRoomType(roomType.Id).ToList();
                return Json(roomRates); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Failed to load room rates.", error = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRate(RoomRateDTO model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                TempData["ErrorMessage"] = "There were errors in your submission. Please check the fields.";


                var rates = _roomRateService.GetAllRoomRates();
                var roomTypes = _roomRateService.GetAllRoomTypes()
                                                .Select(rt => new SelectListItem
                                                {
                                                    Value = rt.Id.ToString(),
                                                    Text = rt.Emer
                                                }).ToList();

                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRateDTO
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        RateMultiplier = rate.RateMultiplier,
                        TipDhomeId = rate.TipDhomeId
                    }).ToList(),
                    RoomTypes = roomTypes,
                    NewRate = model 
                };

                return View("RateView", viewModel);
            }

            _roomRateService.AddRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Name = model.Emer,
                RateMultiplier = model.RateMultiplier,
                TipDhomeId = model.TipDhomeId
            });

            TempData["SuccessMessage"] = "Room Rate added successfully!";
            return RedirectToAction("RateView");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRate(int id)
        {
            var service = _roomRateService.GetRoomRateById(id);
            if (service == null)
            {
                TempData["ErrorMessage"] = "RoomRate not found!";
                return RedirectToAction("RateView");
            }

            _roomRateService.SoftDeleteRoomRate(id);
            TempData["SuccessMessage"] = "Room Rate deleted successfully!";
            return RedirectToAction("RateView");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRate(RoomRateDTO model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); 
                }
                TempData["ErrorMessage"] = "Failed to update the room. Please check the fields.";

                var rates = _roomRateService.GetAllRoomRates();
                var roomTypes = _roomRateService.GetAllRoomTypes()
                                                .Select(rt => new SelectListItem
                                                {
                                                    Value = rt.Id.ToString(),
                                                    Text = rt.Emer
                                                }).ToList();

                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRateDTO
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        RateMultiplier = rate.RateMultiplier,
                        TipDhomeId = rate.TipDhomeId
                    }).ToList(),
                    RoomTypes = roomTypes,
                    NewRate = new RoomRateDTO()
                };

                return View("RateView", viewModel);
            }

            _roomRateService.UpdateRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Id = model.Id,
                Name = model.Emer,
                RateMultiplier = model.RateMultiplier,
                TipDhomeId = model.TipDhomeId
                
            });

            TempData["SuccessMessage"] = "Room Rate updated successfully!";
            return RedirectToAction("RateView");
        }

    }
}

