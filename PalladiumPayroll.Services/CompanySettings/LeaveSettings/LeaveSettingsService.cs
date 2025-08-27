using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings.LeaveSettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.CompanySettings.LeaveSettings;

public class LeaveSettingsService:ILeaveSettingsService
{
    private readonly ILeaveSettingsRepository _leaveSettingsRepository;

    public LeaveSettingsService(ILeaveSettingsRepository leaveSettingsRepository)
    {
        _leaveSettingsRepository = leaveSettingsRepository; 
    }

    public async  Task<JsonResult> GetRulesForLeaveSettings(int companyId, int caseId)
    {
        List<LeaveSettingsResponseDTO>? data = await _leaveSettingsRepository.GetRulesForLeaveSettings(companyId,caseId);

        return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.LeaveSettings, ActionType.Retrieved));
    }
}
