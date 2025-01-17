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
        void Update(RoomRate roomRate); // Add this
    }

    internal class RoomRateRepository : _BaseRepository<RoomRate, int>, IRoomRateRepository
    {
        public RoomRateRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }
        public IEnumerable<RoomRate> GetAll()
        {
            return _dbSet.ToList();
        }
        public new RoomRate GetById(int id)
        {
            return base.GetById(id);
        }

        public void Update(RoomRate roomRate)
        {
            _dbSet.Update(roomRate);
        }



    }
}
