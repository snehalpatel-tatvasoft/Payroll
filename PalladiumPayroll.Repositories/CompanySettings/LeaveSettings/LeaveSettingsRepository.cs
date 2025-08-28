using System.Data;
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
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CaseId", caseId);

        return await _dapper.ExecuteStoredProcedure<LeaveSettingsResponseDTO>("usp_GetLeaveRulesInLeaveSettings", parameters);
    }

    public async Task<bool> UpdateLeaveSettings(LeaveSettingsRequestDTO request)
    {
        var parameters = new DynamicParameters();

        parameters.Add("@LeaveRuleId", request.LeaveRuleId);
        parameters.Add("@CaseId", request.CaseId);

        parameters.Add("@Duration", request.Duration);
        parameters.Add("@LeaveAccumulationDays", request.LeaveAccumulationDays);
        parameters.Add("@ExceedDue", request.ExceedDue);
        parameters.Add("@LeaveCarriedForward", request.LeaveCarriedForward);
        parameters.Add("@LeaveCarriedForwardMaxDays", request.LeaveCarriedForwardMaxDays);
        parameters.Add("@Recurring", request.Recurring);
        parameters.Add("@NoOfTimeReccuring", request.NoOfTimeReccuring);
        parameters.Add("@AnnualEntitlementDays", request.AnnualEntitlementDays);

        // parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

    
        var isSuccess =  await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateLeaveRulesInLeaveSettings", parameters);
        return isSuccess;
    }

}
