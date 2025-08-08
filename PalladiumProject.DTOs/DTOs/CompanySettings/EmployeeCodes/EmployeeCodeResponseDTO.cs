namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;

public class EmployeeCodeResponseDTO
{
    public bool IsAutomatic { get; set; }
    public string InitialCode { get; set; } = string.Empty;
}
