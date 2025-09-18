namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;

public class ImportCommissionRequestDTO
{
    public int CompanyId { get; set; }
    public int CommissionType { get; set; }
    public List<CommissionReportRequestDTO> Commissions { get; set; } = new();
}

public class CommissionReportRequestDTO
{
    public string EmployeeCode { get; set; } = null!;
    public decimal Commission { get; set; }
}

public class ProcessCommissionRequestDTO
{
    public int CompanyId { get; set; }
    public int? CycleId { get; set; }
    public int? PeriodId { get; set; }
    public List<long> CommissionIds { get; set; } = new();
}