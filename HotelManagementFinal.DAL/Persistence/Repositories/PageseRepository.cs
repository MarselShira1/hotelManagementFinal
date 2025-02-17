using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementFinal.Domain.Models;

namespace hotelManagement.DAL.Persistence.Repositories
{
    public interface IPageseRepository : _IBaseRepository<Pagese, int>
    {
        Pagese AddPageseAsync(Pagese pagese);
        
    }

    internal class PageseRepository : _BaseRepository<Pagese, int>, IPageseRepository
    {
        private readonly HotelManagementContext _dbContext;

        public PageseRepository(HotelManagementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Pagese AddPageseAsync(Pagese pagese)
        {
            _dbContext.AddAsync(pagese);
            _dbContext.SaveChanges();
            return pagese;  
        }



    }
}
