using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class DashboardData
    {
        public int TotalBookings { get; set; }
        public List<TopClient> TopClients { get; set; }
        public List<RoomRateStats> RoomRateStats { get; set; }
        public List<RevenueTrend> RevenueTrend { get; set; }
    }
}
