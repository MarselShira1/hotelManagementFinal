using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models
{
    public class CreateRoomRate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? base_price { get; set; }
    }
}
