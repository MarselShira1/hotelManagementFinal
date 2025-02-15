using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hotelManagement.DAL.Persistence.Entities;
using HotelManagementFinal.DAL.Persistence.Repositories;
using HotelManagementFinal.Domain.Models;


namespace HotelManagementFinal.BLL.Services
{
    public interface IDashboardService
    {
        Task<DashboardData> GetDashboardDataAsync(DateOnly  startDate, DateOnly endDate);
    }
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardData> GetDashboardDataAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _dashboardRepository.GetDashboardDataAsync(startDate, endDate);
        }
    }
}
