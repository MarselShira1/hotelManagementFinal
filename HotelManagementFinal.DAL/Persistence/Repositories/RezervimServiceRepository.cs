using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;

namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IRezervimServiceRepository : _IBaseRepository<RezervimService, int>
    {
        IEnumerable<RezervimService> GetByBookingId(int bookingId);
    }

    internal class RezervimServiceRepository : _BaseRepository<RezervimService, int>, IRezervimServiceRepository
    {
        public RezervimServiceRepository(HotelManagementContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<RezervimService> GetByBookingId(int bookingId)
        {
            return _dbSet
                .Where(rs => rs.Rezervim == bookingId && (rs.Invalidated ?? 0) != 0)
                .ToList();
        }
    }
}
