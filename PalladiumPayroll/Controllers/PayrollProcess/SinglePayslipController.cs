using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.SinglePayslip;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;
namespace PalladiumPayroll.Controllers.PayrollProcess;

[Route("api/[controller]")]
[ApiController]
public class SinglePayslipController : ControllerBase
{
    private readonly ISinglePayslipService _singlePayslipService;

    public SinglePayslipController(ISinglePayslipService singlePayslipService)
    {
        _singlePayslipService = singlePayslipService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetEmployeesForProcessing([FromBody] GetEmployeesForProcessingRequestDTO request)
    {
        try
        {
            JsonResult? res = await _singlePayslipService.GetEmployeesForProcessing(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Employees for processing"));
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetPayrollCycleDropdown(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _singlePayslipService.GetPayrollCycleDropdown(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.PayrollCycle)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetNextUnprocessedPeriods(long companyId, long companyPayrollId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _singlePayslipService.GetNextUnprocessedPeriods(companyId, companyPayrollId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Processing Period")
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeRateAndDaysWorked(long employeeId, long processingPeriodId)
    {
        try
        {
            if (employeeId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeNotFound);
            }

            return await _singlePayslipService.GetEmployeeRateAndDaysWorked(employeeId, processingPeriodId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Processing Period")
            );
        }
    }

    #region Transaction
    [HttpGet("[action]")]
    public async Task<ActionResult> GetModalTransactionsListForPayslip(int transactionId, int companyId)
    {
        try
        {
            return await _singlePayslipService.GetModalTransactionsListForPayslip(transactionId, companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Transaction list"));
        }
    }
    #endregion

    [HttpPost("[action]")]
    public async Task<ActionResult> ProcessSinglePayslip([FromBody] ProcessSinglePayslipRequestDTO request)
    {
        try
        {
            return await _singlePayslipService.ProcessSinglePayslip(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, "Payslip"));
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetSinglePayslipDetails(GetSinglePayslipDetailsRequestDTO request)
    {
        try
        {
            return await _singlePayslipService.GetSinglePayslipDetails(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Payslip Details")
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> GetUIFCalculation(GetSinglePayslipDetailsRequestDTO request)
    {
        try
        {
            return await _singlePayslipService.GetUIFCalculation(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "UIF Calculation")
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> DeleteSinglePayslipTransactions(List<PayslipDeleteTransactionDTO> transactions)
    {
        try
        {
            return await _singlePayslipService.DeleteSinglePayslipTransactions(transactions);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, "Payslip Transactions")
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeLeaveDetails(long employeeId, long processingCyclePeriodId)
    {
        try
        {
            if (employeeId <= 0 || processingCyclePeriodId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid employee or processing period ID.");
            }

            return await _singlePayslipService.GetEmployeeLeaveDetails(employeeId, processingCyclePeriodId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Employee Leave Details")
            );
        }
    }
    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeLeaveHistory(long employeeId, long processingCyclePeriodId,long companyId)
    {
        try
        {
            if (employeeId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid employee or processing period ID.");
            }

            return await _singlePayslipService.GetEmployeeLeaveHistory(employeeId, processingCyclePeriodId,companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Employee Leave Details")
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetPayslipPreviewDetails(int payslipPreviewId)
    {
        try
        {
            if (payslipPreviewId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid payslip preview Id.");
            }
            return await _singlePayslipService.GetPayslipPreviewDetails(payslipPreviewId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Employee preview Details")
            );
        }
    }
}
