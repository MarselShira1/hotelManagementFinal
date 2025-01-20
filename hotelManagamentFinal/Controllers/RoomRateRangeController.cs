using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using hotelManagement.BLL.Services;
using HotelManagementFinal.Domain.Models;

namespace HotelManagement.Controllers
{
    public class RoomRateRangeController : Controller
    {
        private readonly IRoomRateRangesService _service;
        private readonly IRoomRateService _roomRateService;


        public RoomRateRangeController(IRoomRateRangesService service, IRoomRateService roomRateService)
        {
            _service = service;
            _roomRateService = roomRateService;
        }
        public IActionResult RoomRateRangeView()
        {
            var roomRateRanges = _service.GetRoomRateRanges();
            var roomRates = _roomRateService.GetAllRoomRates();
            


            var model = new RoomRateRangesViewModel
            {
                RoomRateRanges = roomRateRanges,
                NewRoomRateRange = new RoomRateRange(),
                roomRates = roomRates,
                EditRoomRateRange = new RoomRateRange()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoomRateRange([FromForm] RoomRateRangesViewModel model)
        {
            System.Diagnostics.Debug.WriteLine($"EditRoomRate action hit with model: {model.NewRoomRateRange.Id}");

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
                model.RoomRateRanges = _service.GetRoomRateRanges();
                model.roomRates = _roomRateService.GetAllRoomRates();
                return View("RoomRateRangeView", model);
            }
            try
            {
                if (model.NewRoomRateRange != null)
                {
                    var roomRateRange = new RoomRateRange
                    {
                        RoomRateId = model.NewRoomRateRange.RoomRateId,
                        StartDate = model.NewRoomRateRange.StartDate,
                        EndDate = model.NewRoomRateRange.EndDate,
                        WeekendPricing = model.NewRoomRateRange.WeekendPricing,
                        HolidayPricing = model.NewRoomRateRange.HolidayPricing,
                        Description = model.NewRoomRateRange.Description,
                    };
                    _service.CreateRoomRateRange(roomRateRange);
                }
                return RedirectToAction("RoomRateRangeView");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving to database: " + ex.Message);
                model.RoomRateRanges = _service.GetRoomRateRanges();
                model.roomRates = _roomRateService.GetAllRoomRates();
                return View("RoomRateRangeView", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoomRateRange(RoomRateRange editRoomRateRange)
        {
            System.Diagnostics.Debug.WriteLine($"EditRoomRate action hit with id: {editRoomRateRange.Id}");

            if (ModelState.IsValid)
            {
                _service.UpdateRoomRateRange(editRoomRateRange);
                return RedirectToAction("RoomRateRangeView");
            }


            var model = new RoomRateRangesViewModel
            {
                RoomRateRanges = _service.GetRoomRateRanges(),
                EditRoomRateRange = editRoomRateRange
            };

            return View("RoomRateRangesView", model);
        }
        public IActionResult DeleteRoomRateRange(int id)
        {
            _service.DeleteRoomRateRange(id);
            return RedirectToAction("RoomRateRangeView");
        }

    }

}