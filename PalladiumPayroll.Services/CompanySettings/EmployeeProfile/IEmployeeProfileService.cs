using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Services.CompanySettings.EmployeeProfile;

public interface IEmployeeProfileService
{

    Task<JsonResult> CreateProfile(EmployeeProfileRequestDTO request);
    Task<JsonResult> GetWorkInformatiionDropdownData(int companyId);
    Task<JsonResult> SaveWorkInformation(WorkInformationRequestDTO request);

    Task<JsonResult> GetLeaveRulesForEmployeeProfile(int companyId, int caseId);
    Task<JsonResult> UpdateLeaveSettingsInEmployeeProfile(LeaveSettingsUpdateRequestDTO request);

}
