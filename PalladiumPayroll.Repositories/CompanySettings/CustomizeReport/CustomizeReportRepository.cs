using PalladiumPayroll.DataContext;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.Repositories.CompanySettings;

public class CustomizeReportRepository : ICustomizeReportRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomizeReportRepository(DapperContext dapper, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = dapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<CustomizeReportResponseDTO>> GetAllReports(CustomizeReportRequestDTO request)
    {

        var result = await _dapper.ExecuteStoredProcedure<CustomizeReportResponseDTO>(
            "usp_GetAllReports", null);
        return result.ToList();
    }

    public async Task<DownloadReportResponseDTO?> DownloadReport(DownloadReportRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ReportId", request.ReportId);
        parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);

        return await _dapper.ExecuteStoredProcedureSingle<DownloadReportResponseDTO>(
            "usp_DownloadReport", parameters);
    }

    public async Task<bool> UploadReport(UploadReportRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ReportId", request.ReportId);
        parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64, direction: ParameterDirection.Input);
        parameters.Add("@FilePath", request.FilePath);
        parameters.Add("@FileName", request.FileName);
        parameters.Add("@FileType", request.FileType);
        parameters.Add("@FileSize", request.FileSize);

        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UploadReport", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }
}
