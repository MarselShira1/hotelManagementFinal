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
            var oldRange=_dbSet.RoomRateRanges.Find(roomRateRange.Id);
            oldRange.Invalidated = 0;
            oldRange.ModifiedOn = DateTime.UtcNow;
            oldRange.Description = roomRateRange.Description;
            oldRange.WeekendPricing = roomRateRange.WeekendPricing;
            oldRange.HolidayPricing = roomRateRange.HolidayPricing;
            oldRange.RoomRateId = roomRateRange.RoomRateId;
            oldRange.StartDate = roomRateRange.StartDate;   
            oldRange.EndDate = roomRateRange.EndDate;
            _dbSet.SaveChanges();
        }

        public void CreateRoomRateRange(RoomRateRangeDataAccess roomRateRange)
        {
            try { 
            roomRateRange.CreatedOn = DateTime.Now;
            roomRateRange.Invalidated = 1;
            _dbSet.Add(roomRateRange);
            _dbSet.SaveChanges();
            }catch(Exception ex)
            {

            }
        }

        public void DeleteRoomRateRange(int id)
        {
            var roomRateRange = _dbSet.RoomRateRanges.Find(id);

            if(roomRateRange != null)
            {
                roomRateRange.ModifiedOn = DateTime.UtcNow;
                roomRateRange.CreatedOn = _dbSet.RoomRateRanges.Where(r => r.Id == roomRateRange.Id).First().CreatedOn;
                roomRateRange.Invalidated = 0;
                _dbSet.SaveChanges();
            }

        }

    }
}
