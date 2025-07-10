using System.Net;

namespace PalladiumPayroll.Helper.Middleware.Exceptions.CustomExceptions
{
    [Serializable]
    public class BusinessRuleException : Exception
    {
        public int StatusCode { get; set; }

        public BusinessRuleException(string message)
            : base(message)
        {
        }
        public BusinessRuleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BusinessRuleException(Exception ex, int statuscode = (int)HttpStatusCode.InternalServerError)
            : base(ex.Message)
        {
            StatusCode = statuscode;
        }
    }
}
