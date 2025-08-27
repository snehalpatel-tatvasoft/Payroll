using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.LeaveSettings;

namespace PalladiumPayroll.Repositories.CompanySettings.LeaveSettings;

public class LeaveSettingsRepository : ILeaveSettingsRepository
{
    private readonly DapperContext _dapper;

    public LeaveSettingsRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<List<LeaveSettingsResponseDTO>> GetRulesForLeaveSettings(int companyId, int caseId)
    {
        // if (caseId < 1 || caseId > 8)
        //     throw new ArgumentException("Invalid CaseId. Must be between 1 and 8.", nameof(caseId));

        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CaseId", caseId);

        return await _dapper.ExecuteStoredProcedure<LeaveSettingsResponseDTO>("usp_GetLeaveRulesInLeaveSettings", parameters);
    }

}
