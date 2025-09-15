using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Services.CompanySettings.EmployeeProfile;

public interface IEmployeeProfileService
{

    Task<JsonResult> CreateProfile(EmployeeProfileRequestDTO request);
    Task<JsonResult> GetWorkInformatiionDropdownData(int companyId);
    Task<JsonResult> SaveWorkInformation(WorkInformationRequestDTO request);

    Task<JsonResult> GetLeaveRulesForEmployeeProfile(int companyId, int caseId, long profileId);
    Task<JsonResult> UpdateLeaveSettingsInEmployeeProfile(LeaveSettingsUpdateRequestDTO request);

    Task<JsonResult> GetAllEmployeeProfiles(int companyId);
    Task<JsonResult> DeleteEmployeeProfile(long profileId);
    Task<JsonResult> GetEmployeeProfileDetailsById(long profileId);
    Task<JsonResult> GetModalTransactionsList(int transactionId, int companyId);
    Task<JsonResult> GetTransactionsList(int transactionId, int companyId, int profileId);
    Task<JsonResult> SaveTransactionAssignments(SaveTransactionAssignmentsRequestDTO request);
    Task<JsonResult> DeleteTransactionAssignments(List<long> ids);




}
