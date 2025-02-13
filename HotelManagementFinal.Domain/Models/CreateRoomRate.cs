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
        public decimal base_price { get; set; }
        public int TipDhomeId { get; set; }
        public byte Invalidated { get; set; } = 1;
    }
}
