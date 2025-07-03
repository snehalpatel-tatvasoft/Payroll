namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;

public class DesignationRequestDto
{
    public long? Id { get; set; } = null;
    public string DesignationsName { get; set; } = null!;
    public string DesignationsCode { get; set; } = null!;
    public int CompanyId { get; set; } 
}
