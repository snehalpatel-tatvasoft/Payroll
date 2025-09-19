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

