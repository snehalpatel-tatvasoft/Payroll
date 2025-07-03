namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;

public class ImportDesignationRequestDto
{
    public int CompanyId { get; set; }
    public List<DesignationRequestDto> Designations { get; set; } = new();
}
