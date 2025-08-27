using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings.LeaveSettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class LeaveSettingsController : ControllerBase
{
    private readonly ILeaveSettingsService _leaveSettingsService;

    public LeaveSettingsController(ILeaveSettingsService leaveSettingsService)
    {
        _leaveSettingsService = leaveSettingsService;
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetRulesForLeaveSettings(int companyId, int caseId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            if (caseId < 1 || caseId > 8)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Invalid Case ID.");
            }

            return await _leaveSettingsService.GetRulesForLeaveSettings(companyId,caseId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.LeaveSettings));
        }
    }

}
