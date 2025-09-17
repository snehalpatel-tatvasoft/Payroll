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
}