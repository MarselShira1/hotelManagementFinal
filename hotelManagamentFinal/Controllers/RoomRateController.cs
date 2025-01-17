using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using hotelManagamentFinal.Models.DTO.RoomRate;
using hotelManagement.BLL.Services;

namespace HotelManagement.Controllers
{
    public class RoomRateController : Controller
    {
        private readonly IRoomRateService _roomRateService;

        public RoomRateController(IRoomRateService roomRateService)
        {
            _roomRateService = roomRateService;
        }

        public IActionResult RateView()
        {
            var rates = _roomRateService.GetAllRoomRates();
            var model = new RoomRateViewModel
            {
                Rates = rates.Select(r => new RoomRate
                {
                    Id = r.Id,
                    Emer = r.Emer,
                    CmimBaze = r.CmimBaze
                }).ToList(),
                NewRate = new RoomRate()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRate(RoomRate model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));
                var rates = _roomRateService.GetAllRoomRates();
                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRate
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        CmimBaze = rate.CmimBaze
                    }).ToList(),
                    NewRate = model
                };
                return View("RateView", viewModel);
            }

            _roomRateService.AddRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Name = model.Emer,
                base_price = model.CmimBaze.ToString()
            });

            return RedirectToAction("RateView");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRate(int id)
        {
            var rate = _roomRateService.GetRoomRateById(id);
            if (rate != null)
            {
                _roomRateService.DeleteRoomRate(id);
            }
            return RedirectToAction("RateView");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRate(RoomRate model)
        {
            if (!ModelState.IsValid)
            {
                var rates = _roomRateService.GetAllRoomRates();
                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRate
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        CmimBaze = rate.CmimBaze
                    }).ToList(),
                    NewRate = new RoomRate()
                };
                return View("RateView", viewModel);
            }

            _roomRateService.UpdateRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
            {
                Name = model.Emer,
                base_price = model.CmimBaze.ToString(),
                Id = model.Id // Ensure ID is passed for updating the correct record
            });

            return RedirectToAction("RateView");
        }
    }
}

