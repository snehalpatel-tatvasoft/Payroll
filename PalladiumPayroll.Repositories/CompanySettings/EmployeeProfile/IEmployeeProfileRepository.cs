using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

public interface IEmployeeProfileRepository
{
    Task<(string Message, int EmployeeProfileId)> CreateProfile(EmployeeProfileRequestDTO request);

    Task<JsonResult> GetWorkInformatiionDropdownData(int companyId);
    Task<bool> SaveWorkInformation(WorkInformationRequestDTO request);
    Task<List<TransactionListModel>> GetModalTransactionsList(int transactionId);
    Task<List<TransactionListModel>> GetTransactionsList(int transactionId); // New method


    Task<bool> UpdateLeaveSettingsInEmployeeProfile(LeaveSettingsUpdateRequestDTO request);
    Task<List<LeaveRulesListDTO>> GetLeaveRulesForEmployeeProfile(int companyId, int caseId, long profileId);

    Task<List<EmployeeProfileListDTO>> GetAllEmployeeProfiles(int companyId);
    Task<(bool isSuccess, string message)> DeleteEmployeeProfile(long profileId);

}

