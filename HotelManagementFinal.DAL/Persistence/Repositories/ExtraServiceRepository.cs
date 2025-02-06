using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IExtraServiceRepository : _IBaseRepository<ExtraService, int>
    {
        void Update(ExtraService extraService);
    }

    internal class ExtraServiceRepository : _BaseRepository<ExtraService, int>, IExtraServiceRepository
    {
        public ExtraServiceRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ExtraService> GetAll()
        {
            return _dbSet.Where(es => es.Invalidated != 0).ToList();
        }

        public new ExtraService GetById(int id)
        {
            return base.GetById(id);
        }

        public void Update(ExtraService extraService)
        {
            _dbSet.Update(extraService);
            _dbContext.SaveChanges();
        }
    }
}
