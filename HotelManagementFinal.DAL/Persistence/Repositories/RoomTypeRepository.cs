using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IRoomTypeRepository : _IBaseRepository<TipDhome, int>
    {
        void Delete(TipDhome roomType);
        TipDhome? GetByName(string name);
    }

    internal class RoomTypeRepository : _BaseRepository<TipDhome, int>, IRoomTypeRepository
    {
        public RoomTypeRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }

        public new TipDhome GetById(int id)
        {
            return base.GetById(id);
        }

        public IEnumerable<TipDhome> FilterByName(string name)
        {
            return _dbSet.Where(x => x.Emer.ToLower().Contains(name.ToLower())).ToList();
        }

        public TipDhome? GetByName(string name)
        {
            return _dbSet.FirstOrDefault(x => x.Emer.ToLower() == name.ToLower());
        }

        public void Delete(int id)
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

        public void Delete(TipDhome roomType)
        {
            throw new NotImplementedException();
        }

        //public void Delete(TipDhome roomType)
        // {
        //  throw new NotImplementedException();
        // }
    }
}