using hotelManagement.BLL.Services;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Domain.Models;
using System;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;

namespace hotelManagement.BLL.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync();
        Task AddBookingAsync(Rezervim booking);
        Task<decimal> CalculatePriceAsync(int roomRateId, DateOnly checkIn, DateOnly checkOut);
        Task<IEnumerable<Rezervim>> GetAllBookingsAsync();
        Task<IEnumerable<Rezervim>> GetBookingsByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync()
        {
            return await _bookingRepository.GetAllRoomRatesAsync();
        }

        public async Task AddBookingAsync(Rezervim booking)
        {
            //var totalPrice = await CalculatePriceAsync(booking.RoomRate, booking.CheckIn, booking.CheckOut);
            //booking.Cmim = totalPrice;
            booking.CreatedOn = DateTime.Now;
            booking.Invalidated = 1;

            await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task<decimal> CalculatePriceAsync(int roomRateId, DateOnly checkIn, DateOnly checkOut)
        {
            if (checkIn >= checkOut)
                throw new ArgumentException("Check-out date must be after check-in.");

            var roomRate =  _bookingRepository.GetRoomRateById(roomRateId);
            if (roomRate == null)
                throw new Exception("Room rate not found.");

            var roomRateRanges = await _bookingRepository.GetRoomRateRangesByRoomRateIdAsync(roomRateId);

            decimal totalPrice = 0;
            DateOnly currentDate = checkIn;

            while (currentDate < checkOut)
            {
                
                var applicableRateRange = roomRateRanges
                    .FirstOrDefault(r =>
                        (currentDate >= r.StartDate && currentDate <= r.EndDate) ||
                        (currentDate <= r.EndDate && currentDate >= r.StartDate) ||
                        (currentDate >= r.StartDate && currentDate <= r.EndDate));

                decimal dailyRate = roomRate.CmimBaze; 

                if (applicableRateRange != null)
                {
                    
                    if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dailyRate *= applicableRateRange.WeekendPricing ?? 1; 
                    }
                    if (applicableRateRange.HolidayPricing.HasValue)
                    {
                        dailyRate *= applicableRateRange.HolidayPricing.Value; 
                    }
                }

                totalPrice += dailyRate;
                currentDate = currentDate.AddDays(1);
            }

            return totalPrice;
        }



        public async Task<IEnumerable<Rezervim>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }
        public async Task<IEnumerable<Rezervim>> GetBookingsByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end)
        {
            return await _bookingRepository.GetByRoomAndDateRangeAsync(roomId, start, end);
        }
    }
}
