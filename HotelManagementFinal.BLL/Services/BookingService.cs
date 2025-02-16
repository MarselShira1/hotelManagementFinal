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
        Task<decimal> CalculatePriceAsync(int roomRateId, DateOnly checkIn, DateOnly checkOut);
        Task<IEnumerable<Rezervim>> GetAllBookingsAsync();
        Task<IEnumerable<Rezervim>> GetBookingsByRoomAndDateRangeAsync(int roomId, DateOnly start, DateOnly end);
        Task<RezervimModel> GetRezervimById(int rezervimId);
        Fature AddBill(int rezervimId);
        RezervimModel AddBookingAsync(Rezervim booking);
    }

    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IFatureRepository _fatureRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomTypeService roomTypeService,
            IFatureRepository fatureRepository)
        {
            _bookingRepository = bookingRepository;
            _roomTypeService = roomTypeService;
            _fatureRepository = fatureRepository;
        }


        public async Task<IEnumerable<RoomRate>> GetAllRoomRatesAsync()
        {
            return await _bookingRepository.GetAllRoomRatesAsync();
        }

        public  RezervimModel AddBookingAsync(Rezervim booking)
        {
            booking.CreatedOn = DateTime.Now;
            booking.Invalidated = 1;

            var savedBooking =  _bookingRepository.AddBookingAsync(booking);
             
            var rezervimModel = new RezervimModel
            {
                Id = savedBooking.Id,
                UserId = savedBooking.User,
                DhomeId = savedBooking.Dhome,
                RoomRateId = savedBooking.RoomRate,
                CheckIn = savedBooking.CheckIn,
                CheckOut = savedBooking.CheckOut,
                Cmim = savedBooking.Cmim,
                CreatedOn = savedBooking.CreatedOn,
                ModifiedOn = savedBooking.ModifiedOn,
                Invalidated = savedBooking.Invalidated ?? 0  
            };

            return rezervimModel;
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
              var rezervim  = await _bookingRepository.GetRezervimById(rezervimId);

            if (rezervim == null)
                return null;  

            // Convert Entity to domain model
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


        public Fature AddBill(int rezervimId)
        {
             
           var fature =  _fatureRepository.AddBill(rezervimId); 
            return fature;
        }


    }
}
