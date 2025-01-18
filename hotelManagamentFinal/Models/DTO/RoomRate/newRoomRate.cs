namespace hotelManagamentFinal.Models.DTO.RoomRate
{
    public class newRoomRate
    {
        public string? Emer { get; set; } = null!;

        public string? CmimBaze { get; set; }
    }

    public class RoomRateDTO
    {
        public int Id { get; set; }

        public string Emer { get; set; } = null!;

        public decimal CmimBaze { get; set; }

        public int TipDhomeId { get; set; }
    }
}
