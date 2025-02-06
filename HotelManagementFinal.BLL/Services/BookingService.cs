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

            var roomRate = _bookingRepository.GetRoomRateById(roomRateId);
            if (roomRate == null)
                throw new Exception("Room rate not found.");

            int nights = checkOut.DayNumber - checkIn.DayNumber;
            return nights * roomRate.CmimBaze;
        }

        public async Task<IEnumerable<Rezervim>> GetAllBookingsAsync()
        {
            return await _bookingRepository.GetAllBookingsAsync();
        }
    }
}
