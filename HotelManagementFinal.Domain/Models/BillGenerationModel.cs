using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class GenerateBillModel
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public int roomId { get; set; }
        public int roomRateId { get; set; }

    }
}
