using hotelManagement.Domain.Models;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Common.Exceptions;
using HotelManagementFinal.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace hotelManagement.BLL.Services
{
    public interface IRoomTypeService
    {
        void AddRoomType(CreateRoomType createRoomType);
        void EditRoomType(int id, CreateRoomType createRoomType);
        void RemoveRoomType(int id);
        TipDhome? GetRoomTypeById(int id);
        IEnumerable<TipDhome> GetAllRoomTypes();
        Task<IEnumerable<TipDhome>> GetAllRoomTypesAsync();
        void UpdateRoomType(int id, CreateRoomType createRoomType);
    }

    internal class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository repository)
        {
            roomTypeRepository = repository;
        }


        public void AddRoomType(CreateRoomType createRoomType)
        {
            var existingRoomType = roomTypeRepository.GetByName(createRoomType.Emer);
            if (existingRoomType != null)
            {
                throw new Exception("Room type already exists.");
            }

            var newRoomType = new TipDhome
            {
                Emer = createRoomType.Emer,
                Siperfaqe = createRoomType.Siperfaqe,
                Pershkrim = createRoomType.Pershkrim,
                Kapacitet = createRoomType.Kapacitet
            };

            roomTypeRepository.Add(newRoomType);
            roomTypeRepository.SaveChanges();
        }



        public void EditRoomType(int id, CreateRoomType createRoomType)
        {
            var roomType = roomTypeRepository.GetById(id);

            if (roomType == null)
            {
                throw new Exception("Room type not found.");
            }

            roomType.Emer = createRoomType.Emer;
     
            roomType.Siperfaqe = createRoomType.Siperfaqe;
            roomType.Pershkrim = createRoomType.Pershkrim;
            roomType.Kapacitet = createRoomType.Kapacitet;

            roomTypeRepository.SaveChanges();
        }


        public void RemoveRoomType(int id)
        {
            var roomType = roomTypeRepository.GetById(id);

            if (roomType == null)
            {
                throw new Exception("Room type not found.");
            }

            roomTypeRepository.DeleteById(id);
            roomTypeRepository.SaveChanges();
        }




        public TipDhome? GetRoomTypeById(int id)
        {
            return roomTypeRepository.GetById(id);
        }
      

        // Get all RoomTypes
        public IEnumerable<TipDhome> GetAllRoomTypes()
        {
            return roomTypeRepository.GetAll().ToList(); 
        }

        public void UpdateRoomType(int id, CreateRoomType createRoomType)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TipDhome>> GetAllRoomTypesAsync()
        {

            return await roomTypeRepository.GetAllAsync();
        }
    }
}
