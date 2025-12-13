using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base("resource not found", 404)
        {
        }
        public NotFoundException(string message) : base(message, 404)
        {
        }
      
    }
}
