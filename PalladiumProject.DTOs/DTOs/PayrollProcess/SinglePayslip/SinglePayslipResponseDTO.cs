namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

public class EmployeeForProcessingResponseDTO
{
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; }= string.Empty;
    public string EmployeeName { get; set; }= string.Empty;
    public string EmployeeSurname { get; set; }= string.Empty;
}

public class PayrollCycleDropdownDTO
{
    public long CompanyPayrollId { get; set; }
    public string PayrollCycleName { get; set; } = string.Empty;
}

public class EmployeeRateAndDaysWorkedDto
{
    public decimal RatePerHour { get; set; }
    public int DaysWorked { get; set; }
}

public class ProcessingPeriodDTO
{
    public long ProcessingCyclePeriodId { get; set; }
    public string ProcessPeriod { get; set; } = string.Empty;
}

public class TransactionListModelForPayslip
{     
    public int PayrollProcessId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AllowanceType { get; set; } = string.Empty;
    public int AllowanceTypeId { get; set; }
    public string CalculationType { get; set; } = string.Empty;
    public int FixedAmount { get; set; }
}

public class SinglePayslipDetailsResponseDTO
{
    public long? EmployeePayslipPreviewDtlId { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Hours { get; set; }
    public int PayrollProcessId { get; set; }
    public bool IsRecurring { get; set; }
    public string? CalculationType { get; set; }
    public int FixedAmount { get; set; }

}
public class EmployeeLeaveDetailResponseDTO
{
    public long EmployeeLeaveDetailId { get; set; }
    public long EmployeeId { get; set; }
    public string? LeaveType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Duration { get; set; }
    public int LeaveTypeId { get; set; }
        
}