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

     public interface IFatureRepository : _IBaseRepository<Dhome, int>
    {
        Fature AddBill(int rezervimId);
    }

    internal class FatureRepository : _BaseRepository<Dhome, int>, IFatureRepository
    {
        public FatureRepository(HotelManagementContext dbContext) : base(dbContext)
        {
        }
        public Fature AddBill(int rezervimId)
        {
            try { 
            Fature fature = new Fature
            {
                Invalidated = 1,
                Rezervim = rezervimId,
                Status = "Unpaid",  
                CreatedOn = DateTime.Now
            };
             
              _dbContext.Fatures.Add(fature);
                _dbContext.SaveChanges();
            return fature;
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
