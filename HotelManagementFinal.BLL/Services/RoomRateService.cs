using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Common.Exceptions;
using HotelManagementFinal.Domain.Models;
namespace hotelManagement.BLL.Services
{

    public interface IRoomRateService
    {
        void AddRoomRate(CreateRoomRate roomRate);
        IEnumerable<RoomRate> GetAllRoomRates(); // Change return type to match implementation
        RoomRate GetRoomRateById(int id);
        void DeleteRoomRate(int id);
        void UpdateRoomRate(CreateRoomRate roomRate);
        IEnumerable<RoomType> GetAllRoomTypes();
    }

    internal class RoomRateService : IRoomRateService
    {
        private readonly IRoomRateRepository RoomRateRepository;
        public RoomRateService(IRoomRateRepository repository)
        {
            RoomRateRepository = repository;
        }


        public void AddRoomRate(CreateRoomRate roomRate)
        {
            var roomRateToAdd = new hotelManagement.DAL.Persistence.Entities.RoomRate
            {
                Emer = roomRate.Name,
                CmimBaze = decimal.Parse(roomRate.base_price),
                TipDhomeId = roomRate.TipDhomeId,//statike sa per test,
                CreatedOn = DateTime.Now,
                Invalidated = 1
            };

            RoomRateRepository.Add(roomRateToAdd);
            RoomRateRepository.SaveChanges();
        }


        public IEnumerable<RoomRate> GetAllRoomRates()
        {
            var rates = RoomRateRepository.GetAll()
         .Where(r => r.Invalidated != 0)  // 
         .ToList();

            return rates.Select(rate => new RoomRate
            {
                Id = rate.Id,
                Emer = rate.Emer,
                CmimBaze = rate.CmimBaze,
                TipDhomeId = rate.TipDhomeId
            }).ToList();
        }


        public RoomRate GetRoomRateById(int id)
        {
            var rate = RoomRateRepository.GetById(id);
            return new RoomRate
            {
                Id = rate.Id,
                Emer = rate.Emer,
                CmimBaze = rate.CmimBaze
            };
        }

        public void DeleteRoomRate(int id)
        {
            var rate = RoomRateRepository.GetById(id);
            if (rate != null)
            {
                rate.Invalidated = 0;
                rate.ModifiedOn = DateTime.Now;

                RoomRateRepository.Update(rate);
                RoomRateRepository.SaveChanges();
            }
        }


        public void UpdateRoomRate(CreateRoomRate roomRate)
        {
            var rateToUpdate = RoomRateRepository.GetById(roomRate.Id);
            if (rateToUpdate == null)
                throw new InvalidOperationException("Room rate not found.");


            rateToUpdate.Emer = roomRate.Name;
            rateToUpdate.CmimBaze = decimal.Parse(roomRate.base_price);
            rateToUpdate.TipDhomeId = roomRate.TipDhomeId;

            RoomRateRepository.Update(rateToUpdate);
            RoomRateRepository.SaveChanges();
        }

        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            return RoomRateRepository.GetAllRoomTypes();
        }


    }
}