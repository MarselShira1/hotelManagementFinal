namespace hotelManagamentFinal.Models.DTO.BillGeneration;

public class GenerateBillDto
{
    public DateOnly CheckIn {  get; set; }
    public DateOnly CheckOut { get; set; }
    public int roomId { get; set; }
    public int roomRateId { get; set; }

}
