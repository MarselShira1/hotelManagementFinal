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
namespace hotelManagement.BLL.Services
{
    //

    public interface IRoomService
    {
        void AddBrand(hotelManagement.Domain.Models.CreateRoom room);
        //
    }
    //
    internal class RoomService : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        public RoomService(IRoomRepository repository)
        {
            roomRepository = repository;
        }



        public void EditRoom(int id)
        {
            var room = roomRepository.GetById(id);

            room.Kat = 5;
            room.TipDhome = 1;
            room.NumerDhome = "8";

            roomRepository.SaveChanges();
        }

        public void AddBrand(hotelManagement.Domain.Models.CreateRoom carBrand)
        {
            //EditRoom(3);

            var existingBrand = roomRepository.GetByName(carBrand.RoomNumber);
            if (existingBrand != null)
            {
                throw new CarRentalException("Room already exists");
            }
            var carBrandToAdd = new hotelManagement.DAL.Persistence.Entities.Dhome
            {
                Kat = carBrand.RoomFloor,
                NumerDhome = carBrand.RoomNumber,
                TipDhome = carBrand.RoomTypeId,


            };
            roomRepository.Add(carBrandToAdd);
            roomRepository.SaveChanges();
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