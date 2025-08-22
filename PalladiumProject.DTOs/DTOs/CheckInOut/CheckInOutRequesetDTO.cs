namespace PalladiumPayroll.DTOs.DTOs.CheckInOut;

public class CheckInOutRequesetDTO
{
    public string EmployeeCode { get; set; }="";
    public string Password { get; set; }="";
    public DateTime Date { get; set; }
    public bool IsCheckIn { get; set; }
}
