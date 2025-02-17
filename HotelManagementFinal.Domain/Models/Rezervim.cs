using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models

{
    public class RezervimModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DhomeId { get; set; }
        public string DhomeNumber { get; set; }
        public int RoomRateId { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public decimal Cmim { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public byte Invalidated { get; set; }
    }
}
