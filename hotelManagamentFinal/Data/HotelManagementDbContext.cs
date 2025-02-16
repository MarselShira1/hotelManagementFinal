using hotelManagamentFinal.Models.DTO;
using hotelManagement.DAL.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
namespace HotelManagement.Models
{
    public class HotelManagementDbContext : DbContext
    {
        public DbSet <RoomType1> RoomTypes { get; set; }
        public DbSet <Room> Rooms { get; set; }
        
        public DbSet <RoomRateRangeDataAccess> RoomRateRanges { get; set; }
        public DbSet<RoomRate> RoomRates { get; set; }
        public DbSet<User> Users { get; set; }


        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options) 
        { 

        }
    }
}
