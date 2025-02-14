using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;

namespace hotelManagement.BLL.Services
{
    public interface IRoomTypeService
    {
        void AddRoomType(CreateRoomType roomType);
        IEnumerable<TipDhome> GetAllRoomTypes();
        TipDhome GetRoomTypeById(int id);
        void SoftDeleteRoomType(int id);
        Task<IEnumerable<TipDhome>> GetAllRoomTypesAsync();
        void UpdateRoomType(CreateRoomType roomType);
    }

    internal class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository repository)
        {
            _roomTypeRepository = repository;
        }


        public void AddRoomType(HotelManagementFinal.Domain.Models.CreateRoomType roomType)
        {
            var existing = _roomTypeRepository.GetByName(roomType.Emer);
            if (existing != null)
            {
                throw new Exception("Room type already exists.");
            }

            var newRoomType = new TipDhome
            {
                Emer = roomType.Emer,
                Siperfaqe = roomType.Siperfaqe,
                Pershkrim = roomType.Pershkrim,
                Kapacitet = roomType.Kapacitet,
                CreatedOn = DateTime.Now,
                Invalidated = 1
            };

            _roomTypeRepository.Add(newRoomType);
            _roomTypeRepository.SaveChanges();
        }

        public IEnumerable<TipDhome> GetAllRoomTypes()
        {

            var types = _roomTypeRepository.GetAll()
                .Where(rt => rt.Invalidated != 0)
                .ToList();

            return types.Select(rt => new TipDhome
            {
                Id = rt.Id,
                Emer = rt.Emer,
                Siperfaqe = rt.Siperfaqe ?? 0,
                Pershkrim = rt.Pershkrim,
                Kapacitet = rt.Kapacitet,
                Invalidated = rt.Invalidated
            }).ToList();


        }

        public TipDhome GetRoomTypeById(int id)
        {
            var roomType = _roomTypeRepository.GetById(id);
            if (roomType == null || roomType.Invalidated == 0)
            {
                throw new Exception("Room type not found.");
            }
            return new TipDhome
            {
                Id = roomType.Id,
                Emer = roomType.Emer,
                Siperfaqe = roomType.Siperfaqe ?? 0,
                Pershkrim = roomType.Pershkrim,
                Kapacitet = roomType.Kapacitet,
                Invalidated = roomType.Invalidated
            };
        }

        public void SoftDeleteRoomType(int id)
        {
            var roomType = _roomTypeRepository.GetById(id);
            if (roomType == null)
            {
                throw new Exception("Room type not found.");
            }
            roomType.Invalidated = 0;
            roomType.ModifiedOn = DateTime.Now;
            _roomTypeRepository.Update(roomType);
            _roomTypeRepository.SaveChanges();
        }
        public void UpdateRoomType(CreateRoomType roomType)
        {
            var existing = _roomTypeRepository.GetById(roomType.Id);
            if (existing == null)
            {
                throw new Exception("Room type not found.");
            }

            existing.Emer = roomType.Emer;
            existing.Siperfaqe = roomType.Siperfaqe;
            existing.Pershkrim = roomType.Pershkrim;
            existing.Kapacitet = roomType.Kapacitet;
            existing.ModifiedOn = DateTime.Now;

            _roomTypeRepository.Update(existing);
            _roomTypeRepository.SaveChanges();
        }

        public async Task<IEnumerable<TipDhome>> GetAllRoomTypesAsync()
        {

            return await _roomTypeRepository.GetAllAsync();
        }


    }
}
