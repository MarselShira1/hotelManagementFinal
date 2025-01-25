//using Microsoft.AspNetCore.Mvc;
//using HotelManagement.Models;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace HotelManagement.Controllers
//{
//    public class RoomRateRangeController : Controller
//    {
//        private readonly HotelManagementDbContext _context;

//        public RoomRateRangeController(HotelManagementDbContext context)
//        {
//            _context = context;
//        }
//        public IActionResult RoomRateRangeView()
//        {
//            var roomRateRanges = _context.RoomRateRanges.Include(r => r.RoomRate).ToList();
//            var roomRates = _context.RoomRates.Include(r => r.RoomType).ToList();


//            var model = new RoomRateRangesViewModel
//            {
//                RoomRateRanges = roomRateRanges,
//                NewRoomRateRange = new RoomRateRange(),
//                roomRates = roomRates,
//                EditRoomRateRange = new RoomRateRange()
//            };

//            return View(model);
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateRoomRateRange([FromForm] RoomRateRangesViewModel model)
//        {
//            System.Diagnostics.Debug.WriteLine($"EditRoomRate action hit with model: {model.NewRoomRateRange.Id}");
//            if (!ModelState.IsValid)
//            {
//                foreach (var key in ModelState.Keys)
//                {
//                    var errors = ModelState[key].Errors;
//                    foreach (var error in errors)
//                    {
//                        System.Diagnostics.Debug.WriteLine($"Validation error on {key}: {error.ErrorMessage}");
//                    }
//                }
//                model.RoomRateRanges = _context.RoomRateRanges.Include(r => r.RoomRate).ToList();
//                model.roomRates = _context.RoomRates.Include(r => r.RoomType).ToList();
//                return View("RoomRateRangeView", model);
//            }
//            try
//            {
//                var roomRateRange = new RoomRateRange
//                {
//                    rate_id = model.NewRoomRateRange.rate_id,
//                    StartDate = model.NewRoomRateRange.StartDate,
//                    EndDate = model.NewRoomRateRange.EndDate,
//                    weekend_factor = model.NewRoomRateRange.weekend_factor,
//                    holiday_factor = model.NewRoomRateRange.holiday_factor,
//                    note = model.NewRoomRateRange.note,
//                };
//                _context.RoomRateRanges.Add(roomRateRange);
//                _context.SaveChanges();
//                return RedirectToAction("RoomRateRangeView");
//            }
//            catch (Exception ex)
//            {
//                ModelState.AddModelError("", "Error saving to database: " + ex.Message);
//                model.RoomRateRanges = _context.RoomRateRanges.Include(r => r.RoomRate).ToList();
//                model.roomRates = _context.RoomRates.Include(r => r.RoomType).ToList();
//                return View("RoomRateRangeView", model);
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult EditRoomRateRange(RoomRateRange editRoomRateRange)
//        {
//            System.Diagnostics.Debug.WriteLine($"EditRoomRate action hit with id: {editRoomRateRange.Id}");

//            if (ModelState.IsValid)
//            {
//                var roomRateRange = _context.RoomRateRanges
//                    .Include(r => r.RoomRate)
//                    .FirstOrDefault(r => r.Id == editRoomRateRange.Id);

//                if (roomRateRange != null)
//                {

//                    roomRateRange.StartDate = editRoomRateRange.StartDate;
//                    roomRateRange.EndDate = editRoomRateRange.EndDate;
//                    roomRateRange.weekend_factor = editRoomRateRange.weekend_factor;
//                    roomRateRange.holiday_factor = editRoomRateRange.holiday_factor;
//                    roomRateRange.note = editRoomRateRange.note;

//                    var roomRate = _context.RoomRates
//                        .FirstOrDefault(rr => rr.Id == editRoomRateRange.rate_id);
//                    if (roomRate != null)
//                    {
//                        roomRateRange.RoomRate = roomRate;
//                    }

//                    _context.SaveChanges();
//                }

//                return RedirectToAction("RoomRateRangeView");
//            }


//            var model = new RoomRateRangesViewModel
//            {
//                RoomRateRanges = _context.RoomRateRanges
//                    .Include(r => r.RoomRate)
//                    .ToList(),
//                EditRoomRateRange = editRoomRateRange
//            };

//            return View("RoomRateRangesView", model);
//        }
//        public IActionResult DeleteRoomRateRange(int id)
//        {
//            var roomRateRange = _context.RoomRateRanges.Find(id);
//            if (roomRateRange != null)
//            {
//                _context.RoomRateRanges.Remove(roomRateRange);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("RoomRateRangeView");
//        }

//    }

//}