namespace hotelManagamentFinal.Models.DTO
{
    public class DashboardDto
    {
        public int TotalBookings { get; set; }
        public List<TopClientDto> TopClients { get; set; }
        public List<RoomRateStatsDto> RoomRateStats { get; set; }
        public List<RevenueTrendDto> RevenueTrend { get; set; }
    }
}
