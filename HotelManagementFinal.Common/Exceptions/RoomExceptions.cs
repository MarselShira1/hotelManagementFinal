using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Common.Exceptions
{
    public class CarRentalException(string message) : Exception(message)
    {
    }
}
 //