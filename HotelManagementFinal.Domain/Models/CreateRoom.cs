using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models
{
    public class CreateRoom
    {
        public int RoomTypeId { get; set; }
        public int RoomFloor { get; set; }
        public string RoomNumber { get; set; }
    }
}
