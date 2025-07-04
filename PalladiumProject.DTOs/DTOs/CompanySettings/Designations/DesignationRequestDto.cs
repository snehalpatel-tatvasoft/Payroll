namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;

public class DesignationRequestDto
{
    public long? Id { get; set; } = null;
    public string DesignationsName { get; set; } = null!;
    public string DesignationsCode { get; set; } = null!;
    public int CompanyId { get; set; }
}

public class ImportDesignationRequestDto
{
    public int CompanyId { get; set; }
    public List<DesignationRequestDto> Designations { get; set; } = new();
}

