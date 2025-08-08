namespace PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;

public class EmployeeDropdownDTO
{
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = "";
    public string EmployeeName { get; set; } = "";
    public string EmployeeSurname { get; set; } = "";
}


public class NatureOfGrievanceDTO
{
    public int NatureOfGrievanceId { get; set; }
    public string NatureOfGrievanceType { get; set; } = "";
}


public class EmployeeGrievanceDTO
{
    public long GrievId { get; set; }
    public long EmployeeId { get; set; }
    public string Date { get; set; } = "";
    public string Accused { get; set; } = "";
    public long NatureOfGrievanceId { get; set; }
    public string Stage1Date { get; set; } = "";
    public string Stage1Outcome { get; set; } = "";
    public string Stage1ChairPerson { get; set; } = "";
    public string Stage2Date { get; set; } = "";
    public string Stage2Outcome { get; set; } = "";
    public string Stage2ChairPerson { get; set; } = "";
    public string EmployeeName { get; set; } = "";
    public string NatureOfGrievance { get; set; } = "";
}