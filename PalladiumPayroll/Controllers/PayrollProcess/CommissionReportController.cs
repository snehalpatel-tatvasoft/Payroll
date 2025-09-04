using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.CommissionReport;
using static PalladiumPayroll.Helper.Constants.AppConstants;

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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }

    [HttpGet("[action]")]
    public async Task<JsonResult> PayPeriods(int companyId, int cycleId)
    {
        try
        {
            return await _commissionReportService.GetPayPeriods(companyId, cycleId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }
}
