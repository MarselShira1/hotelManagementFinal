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
        Task<RezervimModel> GetRezervimById(int rezervimId);
        Task<List<CreateRoom>> GetAvailableRooms(DateOnly checkinDate, DateOnly checkoutDate);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomTypeService _roomTypeService;

        public BookingService(IBookingRepository bookingRepository, IRoomTypeService roomTypeService)
        {
            _bookingRepository = bookingRepository;
            _roomTypeService = roomTypeService;
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


            var roomType = _roomTypeService.GetRoomTypeById(roomRate.TipDhomeId);
            if (roomType == null)
                throw new Exception("Room type not found.");


            var roomRateRanges = await _bookingRepository.GetRoomRateRangesByRoomRateIdAsync(roomRateId);

            decimal totalPrice = 0;
            DateOnly currentDate = checkIn;

            while (currentDate < checkOut)
            {

                var applicableRateRange = roomRateRanges
                    .FirstOrDefault(r => currentDate >= r.StartDate && currentDate <= r.EndDate);


                decimal dailyRate = roomType.CmimBaze * roomRate.RateMultiplier;


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

        public async Task<RezervimModel> GetRezervimById(int rezervimId)
        {
            var rezervim = await _bookingRepository.GetRezervimById(rezervimId);

            if (rezervim == null)
                return null;


            return new RezervimModel
            {
                Id = rezervim.Id,
                UserId = rezervim.User,
                DhomeId = rezervim.Dhome,
                RoomRateId = rezervim.RoomRate,
                CheckIn = rezervim.CheckIn,
                CheckOut = rezervim.CheckOut,
                Cmim = rezervim.Cmim,
                CreatedOn = rezervim.CreatedOn,
                ModifiedOn = rezervim.ModifiedOn,
                Invalidated = (byte)rezervim.Invalidated
            };

        }

        public async Task<IEnumerable<Rezervim>> GetBookingsByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end)
        {
            return await _bookingRepository.GetByRoomAndDateRangeAsync(roomId, start, end);
        }

        public async Task<List<CreateRoom>> GetAvailableRooms(DateOnly checkinDate, DateOnly checkoutDate)
        {

            var availableRooms = await _bookingRepository.GetAvailableRooms(checkinDate, checkoutDate);

            var createRooms = availableRooms.Select(r => new CreateRoom
            {
                RoomId = r.Id,
                RoomTypeId = r.TipDhome,
                RoomTypeName = r.TipDhomeNavigation?.Emer,
                RoomFloor = r.Kat,
                RoomNumber = r.NumerDhome,
                Price = r.TipDhomeNavigation != null ? r.TipDhomeNavigation.CmimBaze : 0
            }).ToList();

            return createRooms;
        }
    }
}
