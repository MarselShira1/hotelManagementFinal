namespace hotelManagamentFinal.Models.DTO
{
    public class EmailSendingConfirmationDto
    {
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
