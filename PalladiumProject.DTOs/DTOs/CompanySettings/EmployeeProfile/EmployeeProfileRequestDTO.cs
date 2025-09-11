namespace PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

public class EmployeeProfileRequestDTO
{
    public long? Id { get; set; } = null;
    public string Name { get; set; } = null!;
    public int CompanyId { get; set; }

}
public class WorkInformationRequestDTO
{
    public int ProfileId { get; set; }
    public int CompanyId { get; set; }
    public int DepartmentId { get; set; }
    public int DesignationId { get; set; }
    public int CycleTypeId { get; set; }
    public decimal AnnualSalary { get; set; }
    public decimal MonthlySalary { get; set; }
    public decimal RatePerDay { get; set; }
    public decimal RatePerHour { get; set; }
    public int[] WorkingDays { get; set; } = Array.Empty<int>();
    public decimal HoursPerMonth { get; set; }
    public decimal HoursPerWeek { get; set; }
    public decimal HoursPerDay { get; set; }
    public decimal DayPerMonth { get; set; }
    public decimal DayPerWeek { get; set; }
}