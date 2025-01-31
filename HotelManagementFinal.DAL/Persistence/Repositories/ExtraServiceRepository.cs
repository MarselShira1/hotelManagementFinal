using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IExtraServiceRepository : _IBaseRepository<ExtraService, int>
    {
        void Update(ExtraService extraservice);
    }
    internal class ExtraServiceRepository : _BaseRepository<ExtraService, int>, IExtraServiceRepository
    {
        public ExtraServiceRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }

      //  public IEnumerable<ExtraService> GetAll()
       // {
       //     return _dbSet.ToList();
        //}
        public new ExtraService GetById(int id)
        {
            return base.GetById(id);
        }

        public void Update(ExtraService extraservice)
        {
            _dbSet.Update(extraservice);
        }
    }
}

