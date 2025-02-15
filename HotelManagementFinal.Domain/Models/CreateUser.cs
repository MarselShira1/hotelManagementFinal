using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Emer { get; set; } = string.Empty;
        public string Mbiemer { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool Invalidated { get; set; }
    }
}
