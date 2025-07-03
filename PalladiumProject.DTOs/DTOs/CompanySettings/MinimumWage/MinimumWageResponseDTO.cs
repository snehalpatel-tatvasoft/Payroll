namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

public class MinimumWageResponseDTO
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }=null!;
    public string ProfessionType { get; set; }=null!;
    public decimal MinimumWage { get; set; }
}

