using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;

namespace PalladiumPayroll.Repositories.CompanySettings;

public class EmployeeCodesRepository : IEmployeeCodesRepository
{
    private readonly DapperContext _dapper;

    public EmployeeCodesRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> SaveEmployeeCodeGeneration(EmployeeCodeRequestDTO request)
    {
        DynamicParameters?  parameters = new DynamicParameters();

        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@EmployeeCodeGeneration", request.IsAutomatic ? 1 : 0);
        parameters.Add("@InitialEmployeeCode", string.IsNullOrEmpty(request.InitialCode) ? null : request.InitialCode);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("sp_SaveEmployeeCode", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<EmployeeCodeResponseDTO?> GetEmployeeCodeByCompanyId(int companyId)
    {
        DynamicParameters?  parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeeCodeResponseDTO>(
            "sp_GetEmployeeCode", parameters);
    }
}
