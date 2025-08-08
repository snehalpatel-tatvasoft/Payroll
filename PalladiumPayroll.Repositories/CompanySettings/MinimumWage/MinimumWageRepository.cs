using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings;

public class MinimumWageRepository : IMinimumWageRepository
{
    private readonly DapperContext _dapper;

    public MinimumWageRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> IsDuplicateMinimumWageName(string name, long companyId, int? wageId = null)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@Name", name);
        parameters.Add("@ExcludeWageId", wageId);
        parameters.Add("@IsDuplicate", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_CheckDuplicateMinimumWage", parameters);
        return parameters.Get<bool>("@IsDuplicate");
    }

    public async Task<bool> CreateMinimumWage(MinimumWageRequestDTO request)
    {
        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@Name", request.Name);
        parameters.Add("@ProfessionType", request.ProfessionType);
        parameters.Add("@MinimumWage", request.MinimumWage);
        parameters.Add("@CreatedBy", 1);
        parameters.Add("@CreatedDate", DateTime.Now);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_CreateMinimumWage", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<List<MinimumWageResponseDTO>> GetMinimumWagesByCompanyId(int companyId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<MinimumWageResponseDTO>? result = await _dapper.ExecuteStoredProcedure<MinimumWageResponseDTO>(
            "usp_GetMinimumWageByCompanyId",
            parameters
        );

        return result;
    }

    public async Task<bool> UpdateMinimumWage(MinimumWageRequestDTO request)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@WageId", request.Id);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@Name", request.Name);
        parameters.Add("@ProfessionType", request.ProfessionType);
        parameters.Add("@MinimumWage", request.MinimumWage);
        parameters.Add("@UpdatedBy", 1);
        parameters.Add("@UpdatedDate", DateTime.Now);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpdateMinimumWage", parameters);
        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<bool> DeleteMinimumWage(int wageId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", wageId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteMinimumWage", parameters);
        return parameters.Get<bool>("@IsSuccess");
    }


}
