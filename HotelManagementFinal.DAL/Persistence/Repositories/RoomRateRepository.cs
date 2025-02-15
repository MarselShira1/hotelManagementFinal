﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using iText.Commons.Actions.Contexts;

namespace hotelManagement.DAL.Persistence.Repositories
{

    public interface IRoomRateRepository : _IBaseRepository<RoomRate, int>
    {
        void Update(RoomRate roomRate);
        IEnumerable<TipDhome> GetAllRoomTypes();
        IEnumerable<RoomRate> GetRoomRatesByRoomType(int roomTypeId);

    }

    internal class RoomRateRepository : _BaseRepository<RoomRate, int>, IRoomRateRepository
    {
        public RoomRateRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }
        public IEnumerable<RoomRate> GetAll()
        {
            return _dbSet.Include(r => r.TipDhome).ToList();
        }
        public new RoomRate GetById(int id)
        {
            return base.GetById(id);
        }

        public void Update(RoomRate roomRate)
        {
            _dbSet.Update(roomRate);
            _dbContext.SaveChanges();
        }
        public IEnumerable<TipDhome> GetAllRoomTypes()
        {
            return _dbContext.TipDhomes.ToList(); 
        }
        public IEnumerable<RoomRate> GetRoomRatesByRoomType(int roomTypeId)
        {
           
            return _dbContext.RoomRates
                           .Where(rate => rate.TipDhomeId == roomTypeId && rate.Invalidated != 0)
                           .ToList();
        }

    }
}
