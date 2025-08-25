using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;
using System.Data;

namespace PalladiumPayroll.Repositories.HRFunctions.CoidAccident;

public class CoidAccidentRepository : ICoidAccidentRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CoidAccidentRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;

    }

    public async Task<bool> UpsertCOIDAccident(CoidAccidentRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@Id", request.CoidAccidentId, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);
        parameters.Add("@EmployeeCode", request.EmployeeCode);
        parameters.Add("@AccidentDate", request.AccidentDate);
        parameters.Add("@AccidentType", request.AccidentType);
        parameters.Add("@Severity", request.Severity);
        parameters.Add("@PartOfBodyHurt", request.PartOfBodyHurt);
        parameters.Add("@HoursLost", int.Parse(request.HoursLost));
        parameters.Add("@Reported", request.Reported);
        parameters.Add("@Claimed", request.Claimed);
        parameters.Add("@Settled", request.Settled);
        parameters.Add("@Comments", request.Comments);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@CreatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@LastUpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertCOIDAccident", parameters);
    }
    public async Task<List<CoidAccidentResponseDTO>> GetAccidentsByCompanyId(long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        return (await _dapper.ExecuteStoredProcedure<CoidAccidentResponseDTO>("usp_GetCOIDAccidentsByCompanyId", parameters)).ToList();
    }
    public async Task<bool> DeleteCoidAccident(long coidAccidentId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CoidAccidentId", coidAccidentId);

        var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteCoidAccidentId", parameters);
        return result;
    }

}
