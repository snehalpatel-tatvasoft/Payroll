using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;

namespace PalladiumPayroll.Repositories.CompanySettings;

public class PayslipDisplaySetupRepository : IPayslipDisplaySetupRepository
{
    private readonly DapperContext _dapper;

    public PayslipDisplaySetupRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> SavePayslipDisplaySettings(SavePayslipSettingsDTO request)
    {
        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@IsCompanyContribution", request.IsCompanyContribution ? 1 : 0);
        parameters.Add("@IsFringeBenefits", request.IsFringeBenefits ? 1 : 0);
        parameters.Add("@PayslipLayout", request.PayslipLayout);
        parameters.Add("@PayslipMessage", string.IsNullOrEmpty(request.PayslipMessage) ? null : request.PayslipMessage);
        parameters.Add("@UserId", request.UserId);
        parameters.Add("@RestorePayslipLayout", request.RestorePayslipLayout);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_SavePayslipLayout", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }



    public async Task<PayslipDisplaySetupDataDTO?> GetPayslipSettingsByCompanyId(int companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureSingle<PayslipDisplaySetupDataDTO>(
            "usp_GetPayslipLayoutData", parameters);
    }

}
