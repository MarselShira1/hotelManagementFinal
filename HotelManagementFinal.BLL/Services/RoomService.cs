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
        bool AddBrand(hotelManagement.Domain.Models.CreateRoom room);
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

        public bool AddBrand(hotelManagement.Domain.Models.CreateRoom carBrand)
        {
            try { 
                var existingBrand = roomRepository.GetByName(carBrand.RoomNumber);
                if (existingBrand != null)
                {
                    throw new CarRentalException("Room already exists");
                }
                var carBrandToAdd = new hotelManagement.DAL.Persistence.Entities.Dhome
                {
                    Kat = carBrand.RoomFloor,
                    NumerDhome = carBrand.RoomNumber,
                    TipDhome = (int)carBrand.RoomTypeId,
                    CreatedOn = DateTime.Now,
                    Invalidated = 1

                };
                roomRepository.Add(carBrandToAdd);
                roomRepository.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        //public IEnumerable<Dhome> GetCarBrands()
        //{
        //    var carBrands = roomRepository.GetAll()
        //                                       .ToList();
        //    return carBrands.Select(brand => new Dhome
        //    {
        //        Id = brand.Id,
        //        Name = brand.Name,
        //        LogoPath = brand.Logo
        //    }).ToList();
        //}

        //public Dhome GetById(int id)
        //{
        //    var carBrand = roomRepository.GetById(id);
        //    return
        //        new Dhome
        //        {
        //            Id = carBrand.Id,
        //            Name = carBrand.Name,
        //            LogoPath = carBrand.Logo
        //        };
        //}

        //public void EditBrand(Dhome carBrand)
        //{
        //    var existingBrand = roomRepository.GetById(carBrand.Id);

        //    if (existingBrand == null)
        //    {
        //        throw new CarRentalException("Brand does not exist");
        //    }

        //    existingBrand.Name = carBrand.Name;
        //    roomRepository.SaveChanges();
        //}

        //public void RemoveBrand(int id)
        //{

        //}
    }
}