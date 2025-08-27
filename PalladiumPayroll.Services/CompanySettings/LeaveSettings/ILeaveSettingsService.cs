using Microsoft.AspNetCore.Mvc;

namespace PalladiumPayroll.Services.CompanySettings.LeaveSettings;

public interface ILeaveSettingsService
{
    Task<JsonResult> GetRulesForLeaveSettings(int companyId, int caseId);
}
