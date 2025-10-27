namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

public class GetEmployeesForProcessingRequestDTO
{
    public long PayrollCycleId { get; set; }
    public int EmployeeStatusId { get; set; }
    public int TransactionTypeId { get; set; }
    public long ProcessPeriodId { get; set; }
}

public class ProcessSinglePayslipRequestDTO
{
    public long EmployeeId { get; set; }
    public long CompanyPayrollId { get; set; }
    public long ProcessingCyclePeriodId { get; set; }
    public int PayslipType { get; set; }
    public int StdTrans { get; set; }
    public decimal TotalLeaveDays { get; set; }
    public decimal TotalPresentDays { get; set; }
    public List<PayslipDetailDTO> PayslipDetails { get; set; } = new List<PayslipDetailDTO>();
}

public class PayslipDetailDTO
{
    public long PayrollProcessId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsRecurring { get; set; }
    public decimal? Hours { get; set; }
    public string? ETI { get; set; }
    public string? TransactionType { get; set; }
}

public class TransactionDetailDTO
{
    public long TransactionID { get; set; }
    public string? TransactionName { get; set; }
    public decimal TransactionValue { get; set; }
    public string? TransactionType { get; set; }
}

public class GetSinglePayslipDetailsRequestDTO
{
    public long EmployeeId { get; set; }
    public long CompanyPayrollId { get; set; }
    public long PayrollPeriodId { get; set; }
    public int TransactionTypeId { get; set; }
    public int? NoOfDaysWorked { get; set; }
    public decimal? RatePerHour { get; set; }
    public List<TransactionDetailDTO> TransactionDetails { get; set; } = new();
}

public class PayslipDeleteTransactionDTO
{
    public int EmployeePayslipPreviewDtlId { get; set; }
}


public class EndEmploymentDTO
{
    public int? CompanyPayrollId { get; set; }
    public int? EmpId { get; set; }
    public int? Mode { get; set; }             // 1=Leave Payout, 2=End Emp, 3=Reinstate, 4=Get Period, 5=Check Status
    public DateTime? EndEmpmntDate { get; set; }
    public int? PeriodId { get; set; }
    public string EmpStatus { get; set; } = string.Empty;
    public int? ReinstateType { get; set; }    // 0=New Record, 1=Existing
}