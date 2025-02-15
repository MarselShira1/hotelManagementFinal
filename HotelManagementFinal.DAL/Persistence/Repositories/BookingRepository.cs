﻿using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementFinal.Domain.Models;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Rezervim booking);
        Task<IEnumerable<Rezervim>> GetAllBookingsAsync();
        Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync();
        RoomRate GetRoomRateById(int roomRateId);
        Task<Rezervim> GetRezervimById(int rezervimId);
        Task<IEnumerable<Rezervim>> GetByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end);
        Task<IEnumerable<RoomRateRange>> GetRoomRateRangesByRoomRateIdAsync(int roomRateId);
        Task<List<Rezervim>> GetUserReservations(int userId);
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
        public async Task<IEnumerable<Rezervim>> GetAllBookingsAsync()
        {
            return await _dbContext.Rezervime
                .Include(r => r.DhomeNavigation)
                .Include(r => r.RoomRateNavigation)
                .Where(b => b.Invalidated == 1)
                .ToListAsync();
        }
        public async Task<IEnumerable<Rezervim>> GetByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end)
        {
            return await _dbContext.Rezervime
                .Where(b => b.Dhome == roomId)
                .Where(b => b.CheckIn < end && b.CheckOut > start)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomRateRange>> GetRoomRateRangesByRoomRateIdAsync(int roomRateId)
        {
            var roomRateRangesDataAccess = await _dbContext.RoomRateRanges
                .Where(r => r.RoomRateId == roomRateId && r.Invalidated == 1)
                .ToListAsync();

            var roomRateRanges = roomRateRangesDataAccess.Select(r => new RoomRateRange
            {
                Id = r.Id,
                RoomRateId = r.RoomRateId,
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                WeekendPricing = r.WeekendPricing,
                HolidayPricing = r.HolidayPricing,
                Description = r.Description,
                CreatedOn = r.CreatedOn,
                ModifiedOn = r.ModifiedOn,
                Invalidated = r.Invalidated
            });

            return roomRateRanges;
        }


        public async Task<Rezervim> GetRezervimById(int rezervimId)
        {
            return await _dbContext.Rezervime.Where(w => w.Id == rezervimId).FirstOrDefaultAsync();
        }



        public async Task<List<Rezervim>> GetUserReservations(int userId)
        {
            return await _dbContext.Rezervime.Where(w => w.User == userId && w.Invalidated == 1).ToListAsync();
        }

    }
}
