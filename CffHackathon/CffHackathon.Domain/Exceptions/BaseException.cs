namespace CffHackathon.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public int StatusCode { get; set; }
        public BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public BaseException(string message, Exception innerException, int statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}