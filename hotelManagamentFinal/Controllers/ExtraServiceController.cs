using Microsoft.AspNetCore.Mvc;
using HotelManagement.Models;
using System.Linq;
using hotelManagamentFinal.Models.DTO.ExtraService;
using hotelManagement.BLL.Services;
using HotelManagementFinal.Domain.Models;
using HotelManagementFinal.BLL.Services;
using HotelManagement.Domain.Models;



namespace hotelManagamentFinal.Controllers
{
    public class ExtraServiceController : Controller
    {
        private readonly IExtraServiceService _extraService;

        public ExtraServiceController(IExtraServiceService extraService)
        {
            _extraService = extraService;
        }
        [HttpGet("ExtraServiceView")]
        public IActionResult ExtraServiceView()
        {
            var services = _extraService.GetAllExtraServices();
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
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));

                _extraService.AddExtraService(new CreateExtraService
                {
                    Name = model.Emer,
                    Description = model.Pershkrim,
                });

                var services = _extraService.GetAllExtraServices();
                var viewModel = new ExtraServiceViewModel
                {
                    Services = services.Select(service => new ExtraServiceDTO
                    {
                        Id = service.Id,
                        Emer = service.Emer,
                        Pershkrim = service.Pershkrim
                    }).ToList(),
                    NewService = model
                };
                return View("ExtraServiceView", viewModel);
            }

            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteService(int id)
        {
            var service = _extraService.GetExtraServiceById(id);
            if (service != null)
            {
                _extraService.DeleteExtraService(id);
            }
            return RedirectToAction("ExtraServiceView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditService(ExtraServiceDTO model)
        {
            if (ModelState.IsValid)
            {
                _extraService.UpdateExtraService(new CreateExtraService
                {
                    
                    Name = model.Emer,
                    Description = model.Pershkrim,
                    Id=model.Id
                });

                var services = _extraService.GetAllExtraServices();
                var viewModel = new ExtraServiceViewModel
                {
                    Services = services.Select(service => new ExtraServiceDTO
                    {
                        Id = service.Id,
                        Emer = service.Emer,
                        Pershkrim = service.Pershkrim
                    }).ToList(),
                    NewService = new ExtraServiceDTO()
                };
                return View("ExtraServiceView", viewModel);
            }

            return RedirectToAction("ExtraServiceView");
        }



    }
}
