namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

public class EmployeeForProcessingResponseDTO
{
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeSurname { get; set; } = string.Empty;
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
    public int? PayrollProcessId { get; set; }
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

public class PayslipPreviewHeaderDTO
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyAddress { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;
    public string EmployeeFullName { get; set; } = string.Empty;
    public string JobDesignation { get; set; } = string.Empty;
    public string EmployeeAddress { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PayPeriod { get; set; }
    public string PayType { get; set; } = string.Empty;
    public decimal? TotalEarnings { get; set; }
    public decimal? TotalDeduction { get; set; }
    public decimal? NetPAYE { get; set; }
    public List<PayslipPreviewDetailDTO> Details { get; set; } = new List<PayslipPreviewDetailDTO>();
}

public class PayslipPreviewDetailDTO
{
    public int EmployeePayslipPreviewDtlId { get; set; }
    public int EmployeePayslipPreviewId { get; set; }
    public int AllowanceTypeId { get; set; }
    public string AllowanceTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

public class CalculateLeavePayoutResultDTO
{
    public int EmployeeId { get; set; }
    public int? PayslipId { get; set; }
    public string PayrollCycleType { get; set; } = string.Empty;
    public decimal RatePerDay { get; set; }
    public decimal AnnualLeave { get; set; }
    public decimal DaysDue { get; set; }
    public decimal TotalPresentDays { get; set; }
    public decimal LeavePayoutAmount { get; set; }
    public decimal DueAmountEncashed { get; set; }
    public decimal DueHourEncashed { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}

public class ManageEmploymentStatusResult
{
    public int Result { get; set; }
    public string? ErrorMessage { get; set; }
}