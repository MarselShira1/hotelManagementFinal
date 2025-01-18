using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace hotelManagement.DAL.Persistence.Repositories
{

     public interface IRoomRepository : _IBaseRepository<Dhome, int>
    {
        Dhome? GetByName(string roomNumber);
        Task<IEnumerable<Dhome>> GetAllRoomsAsync();
    }

    internal class RoomRepository : _BaseRepository<Dhome, int>, IRoomRepository
    {
        public RoomRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }
        public new Dhome GetById(int id)
        {
            return base.GetById(id);
        }
        public IEnumerable<Dhome> FilterByName(string numerDhome)
        {
            return _dbSet.Where(x => x.NumerDhome.ToLower().Contains(numerDhome.ToLower())).ToList();
        }


        public Dhome? GetByName(string roomNumber)
        {
            return _dbSet.FirstOrDefault(x => x.NumerDhome.ToLower() == roomNumber.ToLower());
        }

        public async Task<IEnumerable<Dhome>> GetAllRoomsAsync()
        {
            return  base.GetAll();
        }

    }
}
