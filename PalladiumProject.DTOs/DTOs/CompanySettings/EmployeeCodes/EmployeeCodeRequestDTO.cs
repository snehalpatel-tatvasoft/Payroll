namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;

public class EmployeeCodeRequestDTO
{
    public int CompanyId { get; set; }
    public bool IsAutomatic { get; set; }
    public string InitialCode { get; set; }="";
}
