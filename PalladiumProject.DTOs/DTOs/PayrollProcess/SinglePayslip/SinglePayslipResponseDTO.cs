namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

public class SinglePayslipResponseDTO
{

}
public class EmployeeForProcessingResponseDTO
{
    public long EmployeeId { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeSurname { get; set; }
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
    public int ProfileTransactionDetailsId { get; set; }
    public int PayrollProcessId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AllowanceType { get; set; }= string.Empty;
    public int AllowanceTypeId { get; set; }
    public string CalculationType { get; set; }= string.Empty;
    public int FixedAmount { get; set; }
}