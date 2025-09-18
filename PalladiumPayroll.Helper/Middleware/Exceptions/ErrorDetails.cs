namespace PalladiumPayroll.Helper.Middleware.Exceptions
{
    public class ErrorDetails
    {
        public bool Result { get; set; } = false;
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ExceptionDetails { get; set; }
    }
}
