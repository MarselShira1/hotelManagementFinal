using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence;
using HotelManagementFinal.Domain.Models;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardData> GetDashboardDataAsync(DateOnly startDate, DateOnly endDate);
    }
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HotelManagementContext _context;

        public DashboardRepository(HotelManagementContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<DashboardData> GetDashboardDataAsync(DateOnly startDate, DateOnly endDate)
        {
            var totalBookings = await _context.Rezervime
                .Where(r => r.CheckIn >= startDate && r.CheckOut <= endDate)
                .CountAsync();

            var topClients = await _context.Rezervime
                .Where(r => r.CheckIn >= startDate && r.CheckOut <= endDate)
                .GroupBy(r => r.User)
                .Select(g => new TopClient
                {
                    ClientName = _context.Users.Where(u => u.Id == g.Key).Select(u => u.Emer).FirstOrDefault(),
                    TotalBookings = g.Count(),
                    TotalSpending = g.Sum(r => r.Cmim)
                })
                .OrderByDescending(c => c.TotalBookings)
                .Take(5)
                .ToListAsync();

            var roomRateStats = await _context.Rezervime
                .Where(r => r.CheckIn >= startDate && r.CheckOut <= endDate)
                .GroupBy(r => r.RoomRate)
                .Select(g => new RoomRateStats
                {
                    RoomRateName = _context.RoomRates.Where(rr => rr.Id == g.Key).Select(rr => rr.Emer).FirstOrDefault(),
                    TotalBookings = g.Count()
                })
                .ToListAsync();

            var revenueTrend = await _context.Rezervime
                .Where(r => r.CheckIn >= startDate && r.CheckOut <= endDate)
                .GroupBy(r => new { r.CheckIn.Year, r.CheckIn.Month })
                .Select(g => new RevenueTrend
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    TotalRevenue = g.Sum(r => r.Cmim)
                })
                .ToListAsync();

            return new DashboardData
            {
                TotalBookings = totalBookings,
                TopClients = topClients,
                RoomRateStats = roomRateStats,
                RevenueTrend = revenueTrend
            };
        }
    }
}
