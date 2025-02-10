using hotelManagamentFinal.Models.DTO.RoomType;
namespace HotelManagement.Models
{
    public class RoomTypeViewModel
        {
            public List<RoomTypeDTO> RoomTypes { get; set; } = new List<RoomTypeDTO>();
            public RoomTypeDTO NewType { get; set; } = new RoomTypeDTO();
        }
    }
