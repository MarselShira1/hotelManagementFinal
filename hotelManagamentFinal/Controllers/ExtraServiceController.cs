using Microsoft.AspNetCore.Mvc;
using hotelManagement.BLL.Services;
using hotelManagamentFinal.Models.DTO.ExtraService;
using HotelManagement.Models;
using System.Linq;
using hotelManagamentFinal.Models1;
using HotelManagementFinal.BLL.Services;

namespace hotelManagamentFinal.Controllers
{
    public class ExtraServiceController : Controller
    {
        private readonly IExtraServiceService _extraServiceService;

        public ExtraServiceController(IExtraServiceService extraServiceService)
        {
            _extraServiceService = extraServiceService;
        }

        public IActionResult ExtraServiceView()
        {
            var services = _extraServiceService.GetAllExtraServices();

            var model = new ExtraServiceViewModel
            {
                Services = services.Select(s => new ExtraServiceDTO
                {
                    Id = s.Id,
                    Emer = s.Emer,
                    Pershkrim = s.Pershkrim
                }).ToList(),
                NewService = new ExtraServiceDTO()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateService(ExtraServiceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("ExtraServiceView", new ExtraServiceViewModel
                {
                    Services = _extraServiceService.GetAllExtraServices().Select(s => new ExtraServiceDTO
                    {
                        Id = s.Id,
                        Emer = s.Emer,
                        Pershkrim = s.Pershkrim
                    }).ToList(),
                    NewService = model
                });
            }

            _extraServiceService.AddExtraService(new HotelManagementFinal.Domain.Models.CreateExtraService
            {
                Name = model.Emer,
                Description = model.Pershkrim
            });

            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteService(int id)
        {
            _extraServiceService.SoftDeleteExtraService(id);
            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditService(ExtraServiceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("ExtraServiceView", new ExtraServiceViewModel
                {
                    Services = _extraServiceService.GetAllExtraServices().Select(s => new ExtraServiceDTO
                    {
                        Id = s.Id,
                        Emer = s.Emer,
                        Pershkrim = s.Pershkrim
                    }).ToList(),
                    NewService = new ExtraServiceDTO()
                });
            }

            _extraServiceService.UpdateExtraService(new HotelManagementFinal.Domain.Models.CreateExtraService
            {
                Id = model.Id,
                Name = model.Emer,
                Description = model.Pershkrim
            });

            return RedirectToAction("ExtraServiceView");
        }
    }
}
