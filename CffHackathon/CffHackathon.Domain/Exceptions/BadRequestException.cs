namespace CffHackathon.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base("Bad request", 400)
        {
        }

        public BadRequestException(string message) : base(message, 400)
        {
        }
    }
}
