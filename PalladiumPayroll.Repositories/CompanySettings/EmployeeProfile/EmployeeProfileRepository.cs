using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using Dapper;
using System.Data;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;

namespace PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

public class EmployeeProfileRepository : IEmployeeProfileRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;

    public EmployeeProfileRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
    }
    public async Task<string> CreateProfile(EmployeeProfileRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", request.Name);
        parameters.Add("@CompanyID", request.CompanyId);
        parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

        await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CreateEmployeeProfile", parameters);

        return parameters.Get<string>("@ErrorMessage");
    }
}
