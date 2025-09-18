using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public interface ISinglePayslipService
{
    Task<JsonResult> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request);
    Task<JsonResult> GetPayrollCycleDropdown(long companyId);

    Task<JsonResult> GetNextUnprocessedPeriods(long companyId, long companyPayrollId);
    Task<JsonResult> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId);
    
    Task<JsonResult> GetModalTransactionsListForPayslip(int transactionId, int companyId);

    
}
