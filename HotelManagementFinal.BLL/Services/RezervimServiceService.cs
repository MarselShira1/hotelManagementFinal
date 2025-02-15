using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;

namespace HotelManagementFinal.BLL.Services
{
    public interface IRezervimServiceService
    {

        void AddExtraService(int bookingId, int extraServiceId, decimal price);
    }

    internal class RezervimServiceService : IRezervimServiceService
    {
        private readonly IRezervimServiceRepository _rezervimServiceRepository;

        public RezervimServiceService(IRezervimServiceRepository rezervimServiceRepository)
        {
            _rezervimServiceRepository = rezervimServiceRepository;
        }

        public void AddExtraService(int bookingId, int extraServiceId, decimal price)
        {
            
            var rezervimService = new RezervimService
            {
                Rezervim = bookingId,
                Sherbim = extraServiceId,
                Price = price,           
                CreatedOn = DateTime.Now,
                Invalidated = 1         
            };

            
            _rezervimServiceRepository.Add(rezervimService);
            _rezervimServiceRepository.SaveChanges();
        }
    }
}
