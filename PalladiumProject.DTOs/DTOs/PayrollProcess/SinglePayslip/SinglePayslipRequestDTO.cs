namespace PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

public class SinglePayslipRequestDTO
{
}
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
    public long PayrollProcessId { get; set; }
    public decimal Amount { get; set; }
    public bool IsRecurring { get; set; }
    public decimal Hours { get; set; }
}
public class GetSinglePayslipDetailsRequestDTO
{

    public long EmployeeId { get; set; }
    public long CompanyPayrollId { get; set; }
    public long PayrollProcessId { get; set; }
    public int AllowanceTypeId { get; set; }
    public int TransactionTypeId { get; set; }
    public int NoOfDaysWorked { get; set; }
    public decimal RatePerHour { get; set; }
}
