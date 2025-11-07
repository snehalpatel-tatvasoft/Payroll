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
        List<EmployeeForProcessingResponseDTO>? employees = await _singlePayslipRepository.GetEmployeesForProcessing(request);
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
        List<ProcessingPeriodDTO>? processPeriod = await _singlePayslipRepository.GetNextUnprocessedPeriods(companyId, companyPayrollId);
        return HttpStatusCodeResponse.SuccessResponse(processPeriod, string.Format(ResponseMessages.Success, "Process Period", ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId)
    {
        EmployeeRateAndDaysWorkedDto? result = await _singlePayslipRepository.GetEmployeeRateAndDaysWorked(employeeId, processingPeriodId);
        return HttpStatusCodeResponse.SuccessResponse(
            result, string.Format(ResponseMessages.Success, "Rate And Days Worked", ActionType.Retrieved)
        );
    }

    public async Task<JsonResult> GetModalTransactionsListForPayslip(int transactionId, int companyId)
    {
        List<TransactionListModelForPayslip>? transactions = await _singlePayslipRepository.GetModalTransactionsListForPayslip(transactionId, companyId);
        if (transactions.Any())
        {
            return HttpStatusCodeResponse.SuccessResponse(transactions, string.Format(ResponseMessages.Success, "Transaction list", ActionType.Retrieved));
        }
        return HttpStatusCodeResponse.SuccessResponse(new List<TransactionListModelForPayslip>(), "No transactions found for the provided ID.");
    }

    public async Task<JsonResult> ProcessSinglePayslip(ProcessSinglePayslipRequestDTO request)
    {
        long payslipId = await _singlePayslipRepository.ProcessSinglePayslip(request);

        return HttpStatusCodeResponse.SuccessResponse(
            new { EmployeePayslipPreviewId = payslipId },
            string.Format(ResponseMessages.Success, "Payslip Data", ActionType.Saved)
        );
    }

    public async Task<JsonResult> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request)
    {
        List<SinglePayslipDetailsResponseDTO>? result = await _singlePayslipRepository.GetSinglePayslipDetails(request);
        if (result == null)
        {
            return HttpStatusCodeResponse.SuccessResponse(
                new SinglePayslipDetailsResponseDTO(),
                "No payslip details found for the provided parameters."
            );
        }
        return HttpStatusCodeResponse.SuccessResponse(
            result, string.Format(ResponseMessages.Success, "Payslip Details", ActionType.Retrieved)
        );

    }

    public async Task<JsonResult> GetUIFCalculation(GetSinglePayslipDetailsRequestDTO request)
    {
        var (uifCal, uifIncome) = await _singlePayslipRepository.GetUIFCalculation(request);
        var response = new
        {
            UIFCal = uifCal,
            UIFIncome = uifIncome
        };

        return HttpStatusCodeResponse.SuccessResponse(
            response, "UIF calculation retrieved successfully."
        );

    }

    public async Task<JsonResult> DeleteSinglePayslipTransactions(List<PayslipDeleteTransactionDTO> transactions)
    {
        await _singlePayslipRepository.DeleteSinglePayslipTransactions(transactions);
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Deleted));
    }

    public async Task<JsonResult> GetEmployeeLeaveDetails(long employeeId, long processingCyclePeriodId)
    {
        List<EmployeeLeaveDetailResponseDTO>? leaves = await _singlePayslipRepository.GetEmployeeLeaveDetails(employeeId, processingCyclePeriodId);

        if (leaves.Any())
        {
            return HttpStatusCodeResponse.SuccessResponse(
                leaves, string.Format(ResponseMessages.Success, "Employee Leave Details", ActionType.Retrieved)
            );
        }
        return HttpStatusCodeResponse.SuccessResponse(
            new List<EmployeeLeaveDetailResponseDTO>(),
            "No leave records found for the given employee and period."
        );
    }

    public async Task<JsonResult> GetEmployeeLeaveHistory(long employeeId, long processingCyclePeriodId, long companyId)
    {
        List<EmployeeLeaveDetailResponseDTO>? leaves = await _singlePayslipRepository.GetEmployeeLeaveHistory(employeeId, processingCyclePeriodId, companyId);

        if (leaves.Any())
        {
            return HttpStatusCodeResponse.SuccessResponse(
                leaves, string.Format(ResponseMessages.Success, "Employee Leave History", ActionType.Retrieved)
            );
        }
        return HttpStatusCodeResponse.SuccessResponse(
            new List<EmployeeLeaveDetailResponseDTO>(),
            "No leave records found for the given employee and period."
        );
    }

    public async Task<JsonResult> GetPayslipPreviewDetails(int payslipPreviewId)
    {
        PayslipPreviewHeaderDTO? header = await _singlePayslipRepository.GetPayslipPreviewDetails(payslipPreviewId);

        return HttpStatusCodeResponse.SuccessResponse(
            header, string.Format(ResponseMessages.Success, "Employee Preview Details", ActionType.Retrieved)
        );
    }

    public async Task<JsonResult> SaveSinglePayslip(long payslipPreviewId)
    {
        long payslipId = await _singlePayslipRepository.SaveSinglePayslip(payslipPreviewId);

        return HttpStatusCodeResponse.SuccessResponse(
            new { EmployeePayslipPreviewId = payslipId },
            string.Format(ResponseMessages.Success, "Payslip Data", ActionType.Saved)
        );
    }

    public async Task<JsonResult> CalculateLeavePayout(int empId, int companyPayrollId, int periodId)
    {
        CalculateLeavePayoutResultDTO? result = await _singlePayslipRepository.CalculateLeavePayout(empId, companyPayrollId, periodId);

        return HttpStatusCodeResponse.SuccessResponse(result,
            string.Format(ResponseMessages.Success, "Leave Paid Amount", ActionType.Retrieved)
        );
    }

    public async Task<JsonResult> ManageEndEmployment(ManageEndEmploymentDTO request)
    {
        ManageEmploymentStatusResult? res = await _singlePayslipRepository.ManageEndEmployment(request);
        if (res == null || res.Result != 1)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                "Failed to process end employment operation."
            );
        }
        return HttpStatusCodeResponse.SuccessResponse(
         res, string.Format("End Employment processed successfully.")
        );
    }

    public async Task<JsonResult> ReinstateEmployee(ReinstateEmployeeDTO request)
    {
        ManageEmploymentStatusResult? res = await _singlePayslipRepository.ReinstateEmployee(request);
        if (res == null || res.Result != 1)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                "Failed to reinstate the employee."
            );
        }
        return HttpStatusCodeResponse.SuccessResponse(
         res, string.Format("Employee reinstated successfully.")
        );
    }

    public async Task<JsonResult> CheckEndEmploymentStatus(int employeeId)
    {
        int? res = await _singlePayslipRepository.CheckEndEmploymentStatus(employeeId);

        return HttpStatusCodeResponse.SuccessResponse(
         res, string.Format("Employeement status retrived successfully.")
        );
    }

    public async Task<JsonResult> UndoEmployeeSinglePayslip(long employeeId)
    {
        UndoPayslipResult result = await _singlePayslipRepository.UndoEmployeeSinglePayslip(employeeId);

        if (result.Result == "Success")
        {
            return HttpStatusCodeResponse.SuccessResponse(
                result, "Payslip undo operation completed successfully."
            );
        }
        else
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(result.Message ?? "Payslip undo operation failed.");
        }
    }

    public async Task<JsonResult> CheckUndoAvailability(long employeeId)
    {
        int? res = await _singlePayslipRepository.CheckUndoAvailability (employeeId);

        return HttpStatusCodeResponse.SuccessResponse(
         res, string.Format("Undo operation avaibility checked successfully.")
        );
    }

}
