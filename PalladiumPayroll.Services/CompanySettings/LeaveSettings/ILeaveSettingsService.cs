using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;

namespace PalladiumPayroll.Services.CompanySettings.LeaveSettings;

public interface ILeaveSettingsService
{
    Task<JsonResult> GetRulesForLeaveSettings(int companyId, int caseId);

    Task<JsonResult> UpdateLeaveSettings(LeaveSettingsRequestDTO request);
}
