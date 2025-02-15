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
        Dhome GetRoomById(int id);
        bool DeleteRoom(int id);
        
    }
    
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
                RoomTypeId = room.TipDhome,
                RoomTypeName = room.TipDhomeNavigation.Emer, 
                Price = room.TipDhomeNavigation.CmimBaze        
            });
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
                var existingRoom = roomRepository.GetByName(createRoom.RoomNumber);
                if (existingRoom != null)
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

        public Dhome GetRoomById(int id)
        {
            var room = roomRepository.GetById(id);
            if (room == null || room.Invalidated == 0)
            {
                throw new Exception("Room not found.");
            }
            return new Dhome
            {
                Id = room.Id,
                TipDhome = room.TipDhome,  
                Kat = room.Kat,            
                NumerDhome = room.NumerDhome,
                Invalidated = room.Invalidated,
                CreatedOn = room.CreatedOn,
                ModifiedOn = room.ModifiedOn,
                TipDhomeNavigation = room.TipDhomeNavigation,
                Rezervims = room.Rezervims
            };
        }

    }
}