using hotelManagamentFinal.Models.DTO.ExtraService;
using hotelManagamentFinal.Models.DTO;
using HotelManagementFinal.BLL.Services;
using Microsoft.AspNetCore.Mvc; 

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
                TempData["ErrorMessage"] = "There were errors in your submission. Please check the fields.";

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

            TempData["SuccessMessage"] = "Extra Service added successfully!";

            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteService(int id)
        {

            var service = _extraServiceService.GetExtraServiceById(id);
            if (service == null)
            {
                TempData["ErrorMessage"] = "Service not found!";
                return RedirectToAction("ExtraServiceView");
            }
            _extraServiceService.SoftDeleteExtraService(id);

            TempData["SuccessMessage"] = "Extra Service deleted successfully!";

            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditService(ExtraServiceDTO model)
        {
            if (!ModelState.IsValid)

            {
                TempData["ErrorMessage"] = "Failed to update the service. Please check the fields.";


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
            TempData["SuccessMessage"] = "Extra Service updated successfully!";
            return RedirectToAction("ExtraServiceView");
        }

        public IActionResult GetAll()
        {
            var services = _extraServiceService.GetAllExtraServices();
            return Ok(services);
        }
    }
}
