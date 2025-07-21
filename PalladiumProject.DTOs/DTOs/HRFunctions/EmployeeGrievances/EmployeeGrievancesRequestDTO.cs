namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;

public class EmployeeGrievanceUpsertData
{
    public long? EmployeeGrievanceId { get; set; }
    public long EmployeeId { get; set; }
    public long CompanyId { get; set; }
    public DateTime? Date { get; set; }
    public string? Accused { get; set; }=null;
    public long? NatureOfGrievanceId { get; set; }
    public DateTime? Stage1Date { get; set; }
    public string? Stage1Outcome { get; set; }=null;
    public string? Stage1Chairperson { get; set; }=null;
    public DateTime? Stage2Date { get; set; }
    public string? Stage2Outcome { get; set; }=null;
    public string? Stage2Chairperson { get; set; }=null;
    public string UserId { get; set; }="";
}
