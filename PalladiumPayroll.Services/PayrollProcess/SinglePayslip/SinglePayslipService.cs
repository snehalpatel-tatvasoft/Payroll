using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.PayrollProcess.SinglePayslip;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public class SinglePayslipService : ISinglePayslipService
{
    private readonly ISinglePayslipRepository _singlePayslipRepository;

    public SinglePayslipService(ISinglePayslipRepository singlePayslipRepository)
    {
        _singlePayslipRepository = singlePayslipRepository;
    }

    public async Task<JsonResult> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request)
    {
        var employees = await _singlePayslipRepository.GetEmployeesForProcessing(request);
        return HttpStatusCodeResponse.SuccessResponse(employees,
            string.Format(ResponseMessages.Success, "Employees for processing", ActionType.Retrieved));
    }


    public async Task<JsonResult> GetPayrollCycleDropdown(long companyId)
    {
        List<PayrollCycleDropdownDTO> payrollCycle = await _singlePayslipRepository.GetPayrollCycleDropdown(companyId);

        return HttpStatusCodeResponse.SuccessResponse(payrollCycle, string.Format(ResponseMessages.Success, ResponseMessages.PayrollCycle, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetNextUnprocessedPeriods(long companyId, long companyPayrollId)
    {
        var processPeriod = await _singlePayslipRepository.GetNextUnprocessedPeriods(companyId, companyPayrollId);

        return HttpStatusCodeResponse.SuccessResponse(processPeriod, string.Format(ResponseMessages.Success, "Process Period", ActionType.Retrieved));
    }
    public async Task<JsonResult> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId)
    {
        var result = await _singlePayslipRepository.GetEmployeeRateAndDaysWorked(employeeId, processingPeriodId);

        return HttpStatusCodeResponse.SuccessResponse(
            result,
            string.Format(ResponseMessages.Success, "Rate And Days Worked", ActionType.Retrieved)
        );
    }
    public async Task<JsonResult> GetModalTransactionsListForPayslip(int transactionId, int companyId)
    {
        try
        {
            var transactions = await _singlePayslipRepository.GetModalTransactionsListForPayslip(transactionId, companyId);
            if (transactions.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, "Transaction list", ActionType.Retrieved));
            }

            return HttpStatusCodeResponse.SuccessResponse(new List<TransactionListModelForPayslip>(), "No transactions found for the provided ID.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, "Transaction list", ActionType.Retrieving, ex.Message));
        }
    }
    public async Task<JsonResult> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request)
    {
        long payslipId = await _singlePayslipRepository.ProcessSinglePayslip(request);

        return HttpStatusCodeResponse.SuccessResponse(
            new { EmployeePayslipPreviewId = payslipId },
            string.Format(ResponseMessages.Success, "Payslip", ActionType.Created)
        );
    }
    public async Task<JsonResult> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request)
    {
        try
        {
            var result = await _singlePayslipRepository.GetSinglePayslipDetails(request);

            if (result == null || (result.Description == null && result.Amount == null && result.Hours == null))
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    new SinglePayslipDetailsResponseDTO(),
                    "No payslip details found for the provided parameters."
                );
            }

            return HttpStatusCodeResponse.SuccessResponse(
                result,
                string.Format(ResponseMessages.Success, "Payslip Details", ActionType.Retrieved)
            );
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Payslip Details", ex.Message)
            );
        }
    }

}
