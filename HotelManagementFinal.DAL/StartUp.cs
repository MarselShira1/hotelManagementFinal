using hotelManagement.DAL.Persistence;
using hotelManagement.DAL.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.DAL
{  
    public static class Startup
    {
        public static void RegisterDALServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<HotelManagementContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomRateRepository, RoomRateRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<IRoomRateRangesRepository, RoomRateRangesRepository>();
        }

         
    }
}
