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
public class TransactionListModelForPayslip
{
    public int ProfileTransactionDetailsId { get; set; }
    public int PayrollProcessId { get; set; }
    public string Description { get; set; }

    public string AllowanceType { get; set; }
    public int AllowanceTypeId { get; set; }
}
