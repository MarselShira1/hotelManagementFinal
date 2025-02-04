using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.Domain.Models;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Common.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace hotelManagement.BLL.Services
{
  
    public interface IRoomRateService
    {
        void AddRoomRate(hotelManagement.Domain.Models.CreateRoomRate roomRate);
        IEnumerable<RoomRate> GetAllRoomRates(); // Change return type to match implementation
        RoomRate GetRoomRateById(int id);
        void SoftDeleteRoomRate(int id);
        void UpdateRoomRate(CreateRoomRate roomRate);
        IEnumerable<TipDhome> GetAllRoomTypes();
    }
    
    internal class RoomRateService : IRoomRateService
    {
        private readonly IRoomRateRepository RoomRateRepository;
        public RoomRateService(IRoomRateRepository repository)
        {
            RoomRateRepository = repository;
        }


        public void AddRoomRate(hotelManagement.Domain.Models.CreateRoomRate roomRate)
        {
            var roomRateToAdd = new hotelManagement.DAL.Persistence.Entities.RoomRate
            {
                Emer = roomRate.Name,
                CmimBaze = roomRate.base_price,
                TipDhomeId = roomRate.TipDhomeId,
                CreatedOn = DateTime.Now,
                Invalidated = 1
            };

            RoomRateRepository.Add(roomRateToAdd);
            RoomRateRepository.SaveChanges();
        }


        public IEnumerable<RoomRate> GetAllRoomRates()
        {
            var rates = RoomRateRepository.GetAll()
        .Where(r => r.Invalidated != 0)
            .ToList();

            return rates.Select(rate => new RoomRate
            {
                Id = rate.Id,
                Emer = rate.Emer,
                CmimBaze = rate.CmimBaze,
                TipDhomeId = rate.TipDhomeId,
                
                Invalidated = rate.Invalidated
            }).ToList();
        }
        public RoomRate GetRoomRateById(int id)
        {
            var rate = RoomRateRepository.GetById(id);
            return new RoomRate
            {
                Id = rate.Id,
                Emer = rate.Emer,
                CmimBaze = rate.CmimBaze,
                TipDhomeId = rate.TipDhomeId
            };
        }

        public void SoftDeleteRoomRate(int id)
        {
            var rate = RoomRateRepository.GetById(id);
            if (rate != null)
            {
                rate.Invalidated = 0;
                rate.ModifiedOn = DateTime.Now;
                RoomRateRepository.Update(rate);

                RoomRateRepository.SaveChanges();
                Console.WriteLine($"RoomRate {id} marked as deleted. Invalidated: {rate.Invalidated}, ModifiedOn: {rate.ModifiedOn}");
            }
            else
            {
                Console.WriteLine($"RoomRate {id} not found.");
            }
        
        }

        public void UpdateRoomRate(hotelManagement.Domain.Models.CreateRoomRate roomRate)
        {
            var rateToUpdate = RoomRateRepository.GetById(roomRate.Id);
            if (rateToUpdate == null)
                throw new InvalidOperationException("Room rate not found.");


            rateToUpdate.Emer = roomRate.Name;
            rateToUpdate.CmimBaze = roomRate.base_price;
            rateToUpdate.TipDhomeId = roomRate.TipDhomeId;

            rateToUpdate.ModifiedOn = DateTime.Now;

            RoomRateRepository.Update(rateToUpdate);
            RoomRateRepository.SaveChanges();
        }

        public IEnumerable<TipDhome> GetAllRoomTypes()
        {
            return RoomRateRepository.GetAllRoomTypes();
        }

    }
}