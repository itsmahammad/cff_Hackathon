using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Exceptions
{
    public abstract class AggregateExceptionBase(
       IEnumerable<string> errors,
       int statusCode,
       string message = "One or more errors occurred.")
       : BaseException(message, statusCode)
    {
        public IReadOnlyCollection<string> Errors { get; } = errors.ToList().AsReadOnly();
    }
}
