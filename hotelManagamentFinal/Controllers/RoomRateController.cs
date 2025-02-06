using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using hotelManagamentFinal.Models.DTO.RoomRate;
using hotelManagement.BLL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelManagementFinal.BLL.Services;

namespace HotelManagement.Controllers
{
    public class RoomRateController : Controller
    {
        private readonly IRoomRateService _roomRateService;
        private List<SelectListItem> roomTypes;

        public RoomRateController(IRoomRateService roomRateService)
        {
            _roomRateService = roomRateService;
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
                    CmimBaze = r.CmimBaze,
                    TipDhomeId = r.TipDhomeId

                }).ToList(),
                RoomTypes = roomTypes, // per te shtuar select list me tipet e dhomave
                NewRate = new RoomRateDTO()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRate(RoomRateDTO model)
        {
            if (!ModelState.IsValid)
            {
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
                        CmimBaze = rate.CmimBaze,
                        TipDhomeId = rate.TipDhomeId
                    }).ToList(),
                    RoomTypes = roomTypes,
                    NewRate = model // Keep user input in the form
                };

                return View("RateView", viewModel);
            }

            _roomRateService.AddRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Name = model.Emer,
                base_price = model.CmimBaze,
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
                        CmimBaze = rate.CmimBaze,
                        TipDhomeId = rate.TipDhomeId
                    }).ToList(),
                    RoomTypes = roomTypes,
                    NewRate = new RoomRateDTO()
                };

                return View("RateView", viewModel);
            }

            _roomRateService.UpdateRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Name = model.Emer,
                base_price = model.CmimBaze,
                TipDhomeId = model.TipDhomeId,
                Id = model.Id
            });

            TempData["SuccessMessage"] = "Room Rate updated successfully!";
            return RedirectToAction("RateView");
        }

    }
}

