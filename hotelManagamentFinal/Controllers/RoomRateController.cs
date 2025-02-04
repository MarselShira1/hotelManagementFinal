using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using hotelManagamentFinal.Models.DTO.RoomRate;
using hotelManagement.BLL.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                RoomTypes = roomTypes,
                NewRate = new RoomRateDTO()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRate(RoomRateDTO model)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));

                //shtim i room rate
                _roomRateService.AddRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
                {
                    Name = model.Emer,
                    base_price = model.CmimBaze,
                    TipDhomeId = model.TipDhomeId
                });


                //marrja listes me room rate
                var rates = _roomRateService.GetAllRoomRates();
                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRateDTO
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        CmimBaze = rate.CmimBaze
                    }).ToList(),
                    NewRate = model
                };
                return View("RateView", viewModel);
            }

          
            return RedirectToAction("RateView");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRate(int id)
        {
            _roomRateService.SoftDeleteRoomRate(id);
            return RedirectToAction("RateView");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRate(RoomRateDTO model)
        {
            if (ModelState.IsValid)
            {

                _roomRateService.UpdateRoomRate(new hotelManagement.Domain.Models.CreateRoomRate
                {
                    Name = model.Emer,
                    base_price = model.CmimBaze,
                    TipDhomeId = model.TipDhomeId,
                    Id = model.Id 
                });


                var rates = _roomRateService.GetAllRoomRates();
                var viewModel = new RoomRateViewModel
                {
                    Rates = rates.Select(rate => new RoomRateDTO
                    {
                        Id = rate.Id,
                        Emer = rate.Emer,
                        CmimBaze = rate.CmimBaze
                    }).ToList(),
                    NewRate = new RoomRateDTO()
                };
                return View("RateView", viewModel);
            }

            return RedirectToAction("RateView");
        }
    }
}

