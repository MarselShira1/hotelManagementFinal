using hotelManagamentFinal.Models.DTO.RoomType;
namespace hotelManagamentFinal.Models.DTO
{
    public class RoomTypeViewModel
    {
        public List<RoomTypeDTO> RoomTypes { get; set; } = new List<RoomTypeDTO>();
        public RoomTypeDTO NewType { get; set; } = new RoomTypeDTO();
    }
}
