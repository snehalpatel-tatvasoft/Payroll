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

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeesForProcessing([FromQuery] GetEmployeesForProcessingRequestDTO request)
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
    public async Task<ActionResult> GetNextUnprocessedPeriod(long companyId, long companyPayrollId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _singlePayslipService.GetNextUnprocessedPeriod(companyId,companyPayrollId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Processing Period")
            );
        }
    }

}
