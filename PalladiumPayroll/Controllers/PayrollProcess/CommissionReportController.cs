using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.CommissionReport;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.PayrollProcess;

[ApiController]
[Route("api/[controller]")]
public class CommissionReportController : ControllerBase
{
    private readonly ICommissionReportService _commissionReportService;
    public CommissionReportController(ICommissionReportService commissionReportService)
    {
        _commissionReportService = commissionReportService;
    }



    [HttpGet("[action]")]
    public async Task<JsonResult> PayrollCycles(int companyId)
    {
        try
        {
            return await _commissionReportService.GetPayrollCycles(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CommissionReport));
        }
    }

    [HttpGet("[action]")]
    public async Task<JsonResult> PayPeriods(int cycleId)
    {
        try
        {
            return await _commissionReportService.GetPayPeriods(cycleId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CommissionReport));
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ImportCommissions([FromBody] ImportCommissionRequestDTO request)
    {
        try
        {
            var res = await _commissionReportService.ImportCommissions(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Importing, ResponseMessages.CommissionReport));
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetCommissions(int companyId, int? cycleId)
    {
        try
        {
            if (companyId <= 0)
            {
                return BadRequest("Invalid request: CompanyId must be greater than 0.");
            }
            var res = await _commissionReportService.GetCommissions(companyId, cycleId);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CommissionReport));
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> ProcessCommission([FromBody] ProcessCommissionRequestDTO request)
    {
        try
        {
            if (request == null || request.CommissionIds == null || !request.CommissionIds.Any())
            {
                return BadRequest("Invalid request: CommissionIds are required.");
            }
            if (request.CompanyId <= 0)
            {
                return BadRequest("Invalid request: CompanyId must be greater than 0.");
            }
            if (!request.CycleId.HasValue || !request.PeriodId.HasValue)
            {
                return BadRequest("Invalid request: Both cycleId and periodId are required.");
            }
            var res = await _commissionReportService.ProcessCommission(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.CommissionReport));
        }
    }
}
