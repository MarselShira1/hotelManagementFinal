using System.Collections.Generic;
using hotelManagamentFinal.Models.DTO.ExtraService;

namespace hotelManagamentFinal.Models1
{
    public class ExtraServiceViewModel
    {
        public List<ExtraServiceDTO> Services { get; set; } = new List<ExtraServiceDTO>();
        public ExtraServiceDTO NewService { get; set; } = new ExtraServiceDTO();
    }
}
