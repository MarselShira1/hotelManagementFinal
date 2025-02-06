using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.Domain.Models;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using HotelManagementFinal.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;

namespace HotelManagementFinal.BLL.Services
{
    public interface IExtraServiceService
    {
        void AddExtraService(CreateExtraService extraService);
        IEnumerable<ExtraService> GetAllExtraServices();
        ExtraService GetExtraServiceById(int id);
        void SoftDeleteExtraService(int id);
        void UpdateExtraService(CreateExtraService extraService);
    }

    internal class ExtraServiceService : IExtraServiceService
    {
        private readonly IExtraServiceRepository _extraServiceRepository;

        public ExtraServiceService(IExtraServiceRepository repository)
        {
            _extraServiceRepository = repository;
        }

        public void AddExtraService(CreateExtraService extraService)
        {
            var serviceToAdd = new ExtraService
            {
                Emer = extraService.Name,
                Pershkrim = extraService.Description,
                CreatedOn = DateTime.Now,
                Invalidated = 1
            };

            _extraServiceRepository.Add(serviceToAdd);
            _extraServiceRepository.SaveChanges();
        }

        public IEnumerable<ExtraService> GetAllExtraServices()
        {
            return _extraServiceRepository.GetAll();
        }

        public ExtraService GetExtraServiceById(int id)
        {
            return _extraServiceRepository.GetById(id);
        }

        public void SoftDeleteExtraService(int id)
        {
            var service = _extraServiceRepository.GetById(id);
            if (service != null)
            {
                service.Invalidated = 0;
                service.ModifiedOn = DateTime.Now;
                _extraServiceRepository.Update(service);
                _extraServiceRepository.SaveChanges();
            }
        }

        public void UpdateExtraService(CreateExtraService extraService)
        {
            var serviceToUpdate = _extraServiceRepository.GetById(extraService.Id);
            if (serviceToUpdate == null)
                throw new InvalidOperationException("Extra service not found.");

            serviceToUpdate.Emer = extraService.Name;
            serviceToUpdate.Pershkrim = extraService.Description;
            serviceToUpdate.ModifiedOn = DateTime.Now;

            _extraServiceRepository.Update(serviceToUpdate);
            _extraServiceRepository.SaveChanges();
        }
    }
}
