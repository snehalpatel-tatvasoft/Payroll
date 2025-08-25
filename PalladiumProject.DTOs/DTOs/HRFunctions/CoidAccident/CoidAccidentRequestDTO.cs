namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;

public class CoidAccidentRequestDTO
{
    public long? CoidAccidentId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string AccidentDate { get; set; } = string.Empty; 
    public int AccidentType { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string PartOfBodyHurt { get; set; } = string.Empty;
    public string HoursLost { get; set; } = string.Empty; 
    public string Comments { get; set; } = string.Empty;
    public bool Reported { get; set; }
    public bool Claimed { get; set; }
    public bool Settled { get; set; }
    public long CompanyId { get; set; }
}
