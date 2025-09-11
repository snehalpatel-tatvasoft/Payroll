namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

public class EmployeeProfileRequestDTO
{
    public long? Id { get; set; } = null;
    public string Name { get; set; } = null!;
    public int CompanyId { get; set; }

}
