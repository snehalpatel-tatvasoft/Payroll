namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.PayrollProcess;

public class CommissionReportResponseDTO
{
    public long Id { get; set; }
    public string EmployeeCode { get; set; } = null!;
    public string? EmployeeName { get; set; }
    public decimal Commission { get; set; }
}