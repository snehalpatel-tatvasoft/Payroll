using PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;

namespace PalladiumPayroll.Repositories.CompanySettings.LeaveSettings;

public interface ILeaveSettingsRepository
{
    Task<List<LeaveSettingsResponseDTO>> GetRulesForLeaveSettings(int companyId, int caseId);
}
