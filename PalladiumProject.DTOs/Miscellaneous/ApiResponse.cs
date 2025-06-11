namespace PalladiumPayroll.DTOs.Miscellaneous;

public class ApiResponse<T> where T : class
{
    public ApiResponse(bool isSuccess, string message, T data = null, object additionalInfo = null)
    {
        IsSuccess = isSuccess;
        Messages.Add(message);
        Data = data;
        AdditionalInfo = additionalInfo;
    }

    public bool IsSuccess { get; set; } = false;
    public T Data { get; set; }
    public IList<string> Messages { get; set; } = [];
    public object AdditionalInfo { get; set; } = null;
}
