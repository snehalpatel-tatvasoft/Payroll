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
    Task<JsonResult> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request);
    Task<JsonResult> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request);
    Task<JsonResult> GetUIFCalculation(GetSinglePayslipDetailsRequestDTO request);
    Task<JsonResult> DeleteSinglePayslipTransactions(List<PayslipDeleteTransactionDTO> transactions);
    Task<JsonResult> GetEmployeeLeaveDetails(long employeeId, long processingCyclePeriodId);
    Task<JsonResult> GetEmployeeLeaveHistory(long employeeId, long processingCyclePeriodId,long companyId);
    Task<JsonResult> GetPayslipPreviewDetails(int payslipPreviewId);
    Task<JsonResult> SaveSinglePayslip(long payslipPreviewId);
    Task<JsonResult> GetTerminationResons();

}
