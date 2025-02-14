using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class CreateRoomType
    {
        public int Id { get; set; }
        public string Emer { get; set; }
        public decimal Siperfaqe { get; set; }
        public string Pershkrim { get; set; }
        public int Kapacitet { get; set; }
        
    }
}
  