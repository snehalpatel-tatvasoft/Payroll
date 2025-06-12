namespace PalladiumPayroll.DTOs.Miscellaneous;

public class HttpApiResponse<T>
{
    public bool Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}


