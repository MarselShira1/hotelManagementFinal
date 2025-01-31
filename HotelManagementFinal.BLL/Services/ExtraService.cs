using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using HotelManagement.Domain.Models;
using HotelManagementFinal.DAL.Persistence.Repositories;



namespace HotelManagementFinal.BLL.Services
{
    
    public interface IExtraServiceService
    {
        void AddExtraService(CreateExtraService extraService);
        public IEnumerable<ExtraService> GetAllExtraServices();
        public ExtraService GetExtraServiceById(int id);
        void DeleteExtraService(int id);
        void UpdateExtraService(CreateExtraService extraService);
    }
    public class ExtraServiceService : IExtraServiceService  // Change class name
    {
        private readonly IExtraServiceRepository extraServiceRepository;

        public ExtraServiceService(IExtraServiceRepository repository)
        {
            extraServiceRepository = repository;
        }

        public void AddExtraService(CreateExtraService extraService)
        {
            var serviceToAdd = new ExtraService
            {
                Emer = extraService.Name,
                Pershkrim = extraService.Description
            };

            extraServiceRepository.Add(serviceToAdd);
            extraServiceRepository.SaveChanges();
        }

        public IEnumerable<ExtraService> GetAllExtraServices()  // Ensure it's public
        {
            var service = extraServiceRepository.GetAll();
            return service.Select(service => new ExtraService
            {
                Id = service.Id,
                Emer = service.Emer,
                Pershkrim = service.Pershkrim
            }).ToList();
        }

        public ExtraService GetExtraServiceById(int id)
        {
            var service = extraServiceRepository.GetById(id);
            return new ExtraService
            {
                Id = service.Id,
                Emer = service.Emer,
                Pershkrim = service.Pershkrim
            };
        }

        public void DeleteExtraService(int id)
        {
            var service = extraServiceRepository.GetById(id);
            if (service != null)
            {
                extraServiceRepository.Delete(service);
                extraServiceRepository.SaveChanges();
            }
        }

        public void UpdateExtraService(CreateExtraService extraService)
        {
            var serviceToUpdate = extraServiceRepository.GetById(extraService.Id);
            if (serviceToUpdate == null)
                throw new InvalidOperationException("Extra service not found.");

            serviceToUpdate.Emer = extraService.Name;
            serviceToUpdate.Pershkrim = extraService.Description;

            extraServiceRepository.Update(serviceToUpdate);
            extraServiceRepository.SaveChanges();
        }
    }


}
