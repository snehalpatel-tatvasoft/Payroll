using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;

public interface ISinglePayslipRepository
{
    Task<List<EmployeeForProcessingResponseDTO>> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request);
    Task<List<PayrollCycleDropdownDTO>> GetPayrollCycleDropdown(long companyId);

    Task<List<ProcessingPeriodDTO>> GetNextUnprocessedPeriods(long companyId, long companyPayrollId);
    Task<EmployeeRateAndDaysWorkedDto?> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId);
    Task<List<TransactionListModelForPayslip>> GetModalTransactionsListForPayslip(int transactionId, int companyId);
    Task<long> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request);
}
