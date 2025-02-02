using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace hotelManagement.DAL.Persistence.Repositories
{

    public interface IRoomRateRepository : _IBaseRepository<RoomRate, int>
    {
        void Update(RoomRate roomRate);
        IEnumerable<RoomType> GetAllRoomTypes();
    }

    public class RoomType
    {
        public object Id { get; set; }
        public string Emer { get; set; }
    }

    internal class RoomRateRepository : _BaseRepository<RoomRate, int>, IRoomRateRepository
    {
        public RoomRateRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }
        public IEnumerable<RoomRate> GetAll()
        {
            return _dbSet.ToList();
            return _dbSet.Include(r => r.TipDhome).ToList();
        }
        public new RoomRate GetById(int id)
        {
            return base.GetById(id);
        }

        public void Update(RoomRate roomRate)
        {
            _dbSet.Update(roomRate);
            //  _dbContext.SaveChanges(); // Fix: Use the instance of DbContext
        }

        //  public IEnumerable<RoomType> GetAllRoomTypes()
        //  {
        //     return DbContext.RoomTypes.ToList(); // Ensure RoomTypes is in your DbContext
        // }


    }
}