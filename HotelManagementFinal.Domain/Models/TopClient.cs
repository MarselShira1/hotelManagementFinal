using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class TopClient
    {
        public string ClientName { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalSpending { get; set; }
    }
}
