using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Exceptions
{
    internal class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }
        public NotFoundException() : base("resource not found", 404)
        {
        }
    }
}
