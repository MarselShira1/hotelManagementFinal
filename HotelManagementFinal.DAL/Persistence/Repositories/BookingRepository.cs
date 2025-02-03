using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Rezervim booking);
        Task<IEnumerable<Rezervim>> GetAllBookingsAsync();
        Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync();
        RoomRate GetRoomRateById(int roomRateId);
    }

    public class BookingRepository : IBookingRepository
    {
        private readonly HotelManagementContext _dbContext;

        public BookingRepository(HotelManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBookingAsync(Rezervim booking)
        {
            await _dbContext.Rezervime.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rezervim>> GetAllBookingsAsync()
        {
            return await _dbContext.Rezervime
                .Include(r => r.Dhome)
                .Include(r => r.RoomRate)
                .ToListAsync();
        }

        

        public async Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync()
        {
            return await _dbContext.RoomRates
                                   .Where(rate => rate.Invalidated == 1)
                                   .ToListAsync();
        }

        public RoomRate GetRoomRateById(int roomRateId)
        {
            return _dbContext.RoomRates.FirstOrDefault(r => r.Id == roomRateId && r.Invalidated == 1);
        }

    }
}
