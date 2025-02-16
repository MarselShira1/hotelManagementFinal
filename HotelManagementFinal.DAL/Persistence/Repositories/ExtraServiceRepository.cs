using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using hotelManagement.DAL.Persistence.Entities;
using hotelManagement.DAL.Persistence;
using hotelManagement.Domain.Models;
namespace HotelManagementFinal.DAL.Persistence.Repositories
{
    public interface IExtraServiceRepository : _IBaseRepository<ExtraService, int>
    {
        void Update(ExtraService extraService);
        Task<List<ExtraServiceModel>> GetExtraService(int rezervimId);
    }

    internal class ExtraServiceRepository : _BaseRepository<ExtraService, int>, IExtraServiceRepository
    {
        private readonly HotelManagementContext _dbContext;

        public ExtraServiceRepository(HotelManagementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;

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
        //Marsel
        //metoda kthen listen e extra service qe i perkojne nje rezervimi
        public async Task<List<ExtraServiceModel>> GetExtraService(int rezervimId)
        {
            var extraServices = await _dbContext.ExtraServices
                .Join(_dbContext.RezervimServices,
                      es => es.Id,
                      rs => rs.Sherbim,  // Join on ExtraService ID
                      (es, rs) => new { es, rs })
                .Join(_dbContext.Rezervime,
                      rs_es => rs_es.rs.Id,  // Join on Rezervim ID
                      r => r.Id,
                      (rs_es, r) => new { rs_es.es, rs_es.rs, r })
                .Where(joined => joined.r.Id == rezervimId)
                .Select(joined => new ExtraServiceModel
                {
                    Id = joined.es.Id,
                    Name = joined.es.Emer,  // Mapping entity to model
                    Description = joined.es.Pershkrim,
                    Price = joined.rs.Price  // Get price from Rezervim_Service table
                })
                .ToListAsync();

            return extraServices;
        }


    }
}
