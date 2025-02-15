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
using HotelManagementFinal.DAL.Persistence.Repositories;
namespace hotelManagement.BLL.Services
{
    //

    public interface IRoomService
    {
        bool AddRoom(hotelManagement.Domain.Models.CreateRoom room);
        Task<IEnumerable<CreateRoom>> GetRoomsAsync();
        bool EditRoom(hotelManagement.Domain.Models.CreateRoom roomModel);
        Task<CreateRoom> GetRoomById(int roomId);
        bool DeleteRoom(int id);
    }
    //
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IRoomRateRepository _roomRateRepository;
        public RoomService(IRoomRepository repository , IRoomTypeRepository roomTypeRepository, IRoomRateRepository roomRate)
        {
            roomRepository = repository;
            _roomTypeRepository = roomTypeRepository;
            _roomRateRepository = roomRate;
        }

        public bool DeleteRoom(int id)
        {
            try
            {
                var room = roomRepository.GetById(id);
                if(room != null)
                {
                    roomRepository.Delete(room);
                    return true;

                }
                return false;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CreateRoom>> GetRoomsAsync()
        {
            var rooms = await roomRepository.GetAllRoomsAsync();
            return rooms.Select(room => new CreateRoom
            {
                RoomId = room.Id,
                RoomFloor = room.Kat,
                RoomNumber = room.NumerDhome,
                RoomTypeId = room.TipDhome
            });
        }


        public async Task<CreateRoom> GetRoomById(int roomId)
        {
            var room = roomRepository.GetById(roomId);

            if (room == null)
                return null;  

            return new CreateRoom
            {
                RoomId = room.Id,
                RoomTypeId = room.TipDhome,
                RoomTypeName = room.TipDhomeNavigation?.Emer, 
                RoomFloor = room.Kat,
                RoomNumber = room.NumerDhome,
                
            };
        }


        public bool EditRoom(hotelManagement.Domain.Models.CreateRoom roomModel)
        {
            try
            {
                //esteri ndryshoi (roomModel.RoomId ?? 0)
                var room = roomRepository.GetById(roomModel.RoomId);

                if (room != null)
                {
                    room.Kat = roomModel.RoomFloor;
                    room.TipDhome = (int)roomModel.RoomTypeId;
                    room.NumerDhome = roomModel.RoomNumber;
                    room.ModifiedOn = DateTime.Now;
                    roomRepository.SaveChanges();
                    return true;

                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool AddRoom(hotelManagement.Domain.Models.CreateRoom createRoom)
        {
            try { 
                var existingBrand = roomRepository.GetByName(createRoom.RoomNumber);
                if (existingBrand != null)
                {
                    throw new RoomException("Room already exists");
                }
                var createRoomToAdd = new hotelManagement.DAL.Persistence.Entities.Dhome
                {
                    Kat = createRoom.RoomFloor,
                    NumerDhome = createRoom.RoomNumber,
                    TipDhome = (int)createRoom.RoomTypeId,
                    CreatedOn = DateTime.Now,
                    Invalidated = 1

                };
                roomRepository.Add(createRoomToAdd);
                roomRepository.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}