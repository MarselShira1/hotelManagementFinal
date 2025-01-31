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

        bool DeleteRoom(int id);
    }
    //
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        public RoomService(IRoomRepository repository , IRoomTypeRepository roomTypeRepository)
        {
            roomRepository = repository;
            _roomTypeRepository = roomTypeRepository;
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
            var rooms =  await roomRepository.GetAllRoomsAsync();
            var createRooms = new List<CreateRoom>();

            foreach (var room in rooms)
            {
                var roomType =   _roomTypeRepository.GetById(room.TipDhome); 
                createRooms.Add(new CreateRoom
                {
                    RoomId = room.Id,
                    RoomFloor = room.Kat,
                    RoomNumber = room.NumerDhome,
                    RoomTypeId = room.TipDhome,
                    RoomTypeName = roomType?.Emer
                });
            }
            return createRooms;

        }




        public bool EditRoom(hotelManagement.Domain.Models.CreateRoom roomModel)
        {
            try
            {
                var room = roomRepository.GetById(roomModel.RoomId ?? 0);

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