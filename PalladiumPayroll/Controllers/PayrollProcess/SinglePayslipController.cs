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
    public async Task<ActionResult> GetEmployeeLeaveHistory(long employeeId, long processingCyclePeriodId, long companyId)
    {
        try
        {
            if (employeeId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid employee or processing period ID.");
            }

            return await _singlePayslipService.GetEmployeeLeaveHistory(employeeId, processingCyclePeriodId, companyId);
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

    [HttpPost("[action]")]
    public async Task<ActionResult> SaveSinglePayslip(long employeePayslipPreviewId)
    {
        try
        {
            return await _singlePayslipService.SaveSinglePayslip(employeePayslipPreviewId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, "Payslip"));
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> CalculateLeavePayout(int empId, int companyPayrollId, int periodId)
    {
        try
        {
            return await _singlePayslipService.CalculateLeavePayout(empId, companyPayrollId, periodId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Leave paid amount"));
        }
    }


    [HttpPost("[action]")]
    public async Task<JsonResult> ManageEndEmployment([FromBody] ManageEndEmploymentDTO request)
    {
        try
        {
            if (request == null)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Request data is missing.");
            }

            return await _singlePayslipService.ManageEndEmployment(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("An Exception occures while process end employment.");
        }
    }

    [HttpPost("[action]")]
    public async Task<JsonResult> ReinstateEmployee([FromBody] ReinstateEmployeeDTO request)
    {
        try
        {
            if (request == null)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Request data is missing.");
            }

            return await _singlePayslipService.ReinstateEmployee(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("An Exception occures while reinstate the employee.");
        }
    }


    [HttpGet("[action]")]
    public async Task<JsonResult> CheckEndEmploymentStatus(int employeeId)
    {
        try
        {
            if (employeeId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid Employee Id.");
            }

            return await _singlePayslipService.CheckEndEmploymentStatus(employeeId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("An Exception occures while retrieving employment status.");
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UndoEmployeeSinglePayslip([FromBody]UndoPayslipRequestDTO dto)
    {
        try
        {
            if (dto==null || dto.EmployeeId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid Employee Id.");
            }  
            return await _singlePayslipService.UndoEmployeeSinglePayslip(dto);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("An exception occurred while undoing the employee payslip.");
        }
    }

    [HttpGet("[action]")]
    public async Task<JsonResult> CheckUndoRedoAvailability(long employeeId)
    {
        try
        {
            if (employeeId <= 0)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid Employee Id.");
            }

            return await _singlePayslipService.CheckUndoRedoAvailability(employeeId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse("An Exception occures while retrieving undo/redo availability.");
        }
    }

}
