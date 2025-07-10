using System.Net;

namespace PalladiumPayroll.Helper.Middleware.Exceptions.CustomExceptions
{
    public class NoContentException : Exception
    {
        public int StatusCode { get; set; }
        public NoContentException()
        {
        }

        public NoContentException(string message)
            : base(message)
        {
        }
        public NoContentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NoContentException(Exception ex, int statuscode = (int)HttpStatusCode.NoContent)
            : base(ex.Message)
        {
            StatusCode = statuscode;
        }
    }
}
