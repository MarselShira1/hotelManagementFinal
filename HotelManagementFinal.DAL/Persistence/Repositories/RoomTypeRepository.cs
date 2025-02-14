using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IRoomTypeRepository : _IBaseRepository<TipDhome, int>
    {
        TipDhome? GetByName(string name);
        void Update(TipDhome roomType);
        IEnumerable<TipDhome> GetAllRoomTypes();
        Task<IEnumerable<TipDhome>> GetAllAsync();

    }

    internal class RoomTypeRepository : _BaseRepository<TipDhome, int>, IRoomTypeRepository
    {
        public RoomTypeRepository(HotelManagementContext dbContext) : base(dbContext) { }

        public IEnumerable<TipDhome> GetAllRoomTypes()
        {
            return _dbContext.TipDhomes.ToList();
        }

        public TipDhome? GetByName(string name)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(rt => rt.Emer.ToLower() == name.ToLower());
        }

        public void Update(TipDhome roomType)
        {
            _dbSet.Update(roomType);
            _dbContext.SaveChanges();
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