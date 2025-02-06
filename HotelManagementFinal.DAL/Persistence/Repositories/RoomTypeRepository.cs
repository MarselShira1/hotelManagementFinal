using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IRoomTypeRepository : _IBaseRepository<TipDhome, int>
    {
        TipDhome? GetByName(string name);
        void DeleteById(int id);
        Task<IEnumerable<TipDhome>> GetAllAsync();

    }

    internal class RoomTypeRepository : _BaseRepository<TipDhome, int>, IRoomTypeRepository
    {
        public RoomTypeRepository(HotelManagementContext dbContext) : base(dbContext) { }
       
       
          public TipDhome? GetByName(string name)
        {
            return _dbSet
                .AsNoTracking()
                .FirstOrDefault(x => x.Emer.ToLower() == name.ToLower());
        }

        public void DeleteById(int id)
        {
            var roomType = _dbSet.Find(id);
            if (roomType != null)
            {
                _dbSet.Remove(roomType);
                SaveChanges();
            }
            else
            {
                throw new Exception("Room type not found");
            }
        }
        public async Task<IEnumerable<TipDhome>> GetAllAsync()
        {
            var rooms = await _dbSet
                .AsNoTracking()
                .ToListAsync();

            Console.WriteLine($" Repository: Found {rooms.Count} room types.");
            return rooms;
        
        }


    }
}