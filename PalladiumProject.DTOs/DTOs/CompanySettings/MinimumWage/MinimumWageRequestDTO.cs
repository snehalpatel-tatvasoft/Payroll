namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

public class MinimumWageRequestDTO
{
    public int? Id { get; set; }
    public long CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string ProfessionType { get; set; } = null!;
    public decimal MinimumWage { get; set; }
}
