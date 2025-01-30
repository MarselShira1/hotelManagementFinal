using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementFinal.Domain.Models
{
    public class AuthModel
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int Role { get; set; }
    }
}
