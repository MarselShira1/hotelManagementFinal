using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class CreateExtraService
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte Invalidated { get; set; } = 1;
    }
}
