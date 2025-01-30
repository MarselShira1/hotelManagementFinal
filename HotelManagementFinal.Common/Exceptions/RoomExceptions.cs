using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelManagement.Common.Exceptions
{
    public class RoomException(string message) : Exception(message)
    {
    }
}
 //