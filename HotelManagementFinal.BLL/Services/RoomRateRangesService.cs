using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.Common.Exceptions;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence.Repositories;
using hotelManagement.Domain.Models;
using HotelManagementFinal.Domain.Models;

namespace hotelManagement.BLL.Services
{
    public interface IRoomRateRangesService
    {
        public List<RoomRateRange> GetRoomRateRanges();
        public void DeleteRoomRateRange(int id);
        public void CreateRoomRateRange(RoomRateRange range);
        public void UpdateRoomRateRange(RoomRateRange range);
    }

    internal class RoomRateRangesService : IRoomRateRangesService
    {
        private readonly IRoomRateRangesRepository roomRateRangesRepository;
        public RoomRateRangesService(IRoomRateRangesRepository repository)
        {
            roomRateRangesRepository = repository;
        }

        public List<RoomRateRange> GetRoomRateRanges()
        {
            List<RoomRateRange> list = new List<RoomRateRange>();
            List<RoomRateRangeDataAccess> data = roomRateRangesRepository.GetAll();

            foreach (RoomRateRangeDataAccess dataAccess in data)
                list.Add(MapForBll(dataAccess));
            return list;
        }

        public void DeleteRoomRateRange(int id)
        {
            roomRateRangesRepository.DeleteRoomRateRange(id);
        }

        public void CreateRoomRateRange(RoomRateRange range1)
        {
            roomRateRangesRepository.CreateRoomRateRange(new RoomRateRangeDataAccess()
            {
                Id = range1.Id,
                RoomRateId = range1.RoomRateId,
                StartDate = range1.StartDate,
                EndDate = range1.EndDate,
                WeekendPricing = range1.WeekendPricing,
                HolidayPricing = range1.HolidayPricing,
                Description = range1.Description,
                CreatedOn = range1.CreatedOn,
                ModifiedOn = range1.ModifiedOn,
                Invalidated = range1.Invalidated
            });
        }

        public void UpdateRoomRateRange(RoomRateRange range1)
        {
            roomRateRangesRepository.UpdateRoomRateRange(new RoomRateRangeDataAccess()
            {
                Id = range1.Id,
                RoomRateId = range1.RoomRateId,
                StartDate = range1.StartDate,
                EndDate = range1.EndDate,
                WeekendPricing = range1.WeekendPricing,
                HolidayPricing = range1.HolidayPricing,
                Description = range1.Description,
                CreatedOn = range1.CreatedOn,
                ModifiedOn = range1.ModifiedOn,
                Invalidated = range1.Invalidated
            });
        }

        private RoomRateRange MapForBll(RoomRateRangeDataAccess range1)
        {
            return new RoomRateRange()
            {
                Id=range1.Id,
                RoomRateId=range1.RoomRateId,
                StartDate=range1.StartDate,
                EndDate=range1.EndDate,
                WeekendPricing=range1.WeekendPricing,
                HolidayPricing=range1.HolidayPricing,
                Description=range1.Description,
                CreatedOn=range1.CreatedOn,
                ModifiedOn=range1.ModifiedOn,
                Invalidated=range1.Invalidated,
                RoomRate = range1.RoomRate == null ? null : new CreateRoomRate
                {
                    Id = range1.RoomRate.Id,
                    Name = range1.RoomRate.Emer,
                    RateMultiplier = range1.RoomRate.RateMultiplier,
                    TipDhomeId = range1.RoomRate.TipDhomeId,
                }
            };
        }
    }
}
