using hotelManagement.BLL.Services;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Domain.Models;
using System;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;
using System.Reflection.Metadata;
using Stripe;

namespace hotelManagement.BLL.Services
{
    public interface IPageseService
    { 
        bool AddPageseAsync(PageseModel pagese);
        void SaveChanges();
    }

    public class PageseService : IPageseService
    {
        private readonly IPageseRepository _pageseRepository
            ;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IFatureRepository _fatureRepository;

        public PageseService(IPageseRepository pageseRepository, IRoomTypeService roomTypeService,
            IFatureRepository fatureRepository)
        {
            _pageseRepository = pageseRepository;
            _roomTypeService = roomTypeService;
            _fatureRepository = fatureRepository;
        } 
        public bool AddPageseAsync(PageseModel pagese)
        {
            Pagese pagesa = new Pagese
            {
                Fature = pagese.fatureId,
                Menyre = "Card",
                CreatedOn = DateTime.Now,
                Total = pagese.cmimi,
                Invalidated = 1

            };


            var savedBooking =  _pageseRepository.AddPageseAsync(pagesa);

            if(savedBooking != null)
            {
                return true;
            }
            return false;   
        }

       public void SaveChanges()
        {
            _pageseRepository.SaveChanges();
        }
    }
}
