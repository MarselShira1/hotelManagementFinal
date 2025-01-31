using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IRoomRateRangesRepository : _IBaseRepository<RoomRateRangeDataAccess, int>
    {
        void UpdateRoomRateRange(RoomRateRangeDataAccess roomRateRange);
        void CreateRoomRateRange(RoomRateRangeDataAccess roomRateRange);
        public List<RoomRateRangeDataAccess> GetAll();
        void DeleteRoomRateRange(int id);

    }

    internal class RoomRateRangesRepository : _BaseRepository<RoomRateRangeDataAccess, int>, IRoomRateRangesRepository
    {
        private readonly HotelManagementContext _dbSet;
        public RoomRateRangesRepository(HotelManagementContext dbContext) : base(dbContext)
        {
            _dbSet = dbContext;
        }
        public List<RoomRateRangeDataAccess> GetAll()
        {
            List < RoomRateRangeDataAccess > lista= _dbSet.RoomRateRanges.ToList();
            return lista;
        }

        public void UpdateRoomRateRange(RoomRateRangeDataAccess roomRateRange)
        {
            _dbSet.Update(roomRateRange);
            _dbSet.SaveChanges();
        }

        public void CreateRoomRateRange(RoomRateRangeDataAccess roomRateRange)
        {
            _dbSet.Add(roomRateRange);
            _dbSet.SaveChanges();
        }

        public void DeleteRoomRateRange(int id)
        {
            var roomRateRange = _dbSet.RoomRateRanges.Find(id);

            if(roomRateRange != null)
            {
                _dbSet.RoomRateRanges.Remove(roomRateRange);
                _dbSet.SaveChanges();
            }

        }

    }
}
