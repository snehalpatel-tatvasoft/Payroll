namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;

public class DesignationRequestDTO
{
    public long? Id { get; set; } = null;
    public string DesignationsName { get; set; } = null!;
    public string DesignationsCode { get; set; } = null!;
    public int CompanyId { get; set; }
}

public class ImportDesignationRequestDTO
{
    public int CompanyId { get; set; }
    public List<DesignationRequestDTO> Designations { get; set; } = new();
}

