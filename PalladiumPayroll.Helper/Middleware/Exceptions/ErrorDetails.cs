namespace PalladiumPayroll.Helper.Middleware.Exceptions
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ExceptionDetails { get; set; }
    }
}
