using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models
{
    public class ExtraServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Mapped from "Emer"
        public string? Description { get; set; }  // Mapped from "Pershkrim"
        public decimal Price { get; set; }  // Price from Rezervim_Service table

    }
}
