namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs
{
    public class CommonResponse<T>
    {
        public T? data { get; set; }

        public string success_message { get; set; } = string.Empty;

        public string error_message { get; set; } = string.Empty;
    }
}
