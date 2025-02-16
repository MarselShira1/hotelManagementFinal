namespace hotelManagamentFinal.Models.DTO
{
    public class NewBookingDTO
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public int RoomRateId { get; set; }
        //public string Email { get; set; }
        public decimal Price { get; set; }

        public DateOnly CheckIn { get; set; }

        public DateOnly CheckOut { get; set; }
        public decimal totalPrice { get; set; }
    }
}
