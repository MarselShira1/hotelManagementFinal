using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models
{
    public class CreateRoom
    {
        //esteri hoqi nullable nga RoomId :)
        public int RoomId { get; set; }
        public int? RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public int? RoomFloor { get; set; }
        public string? RoomNumber { get; set; }
    }
}
