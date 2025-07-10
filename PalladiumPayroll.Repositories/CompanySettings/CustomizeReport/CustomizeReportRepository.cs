using PalladiumPayroll.DataContext;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings;

public class CustomizeReportRepository : ICustomizeReportRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;

    public CustomizeReportRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
    }

    public async Task<List<CustomizeReportResponseDTO>> GetAllReports(CustomizeReportRequestDTO request)
    {

        var result = await _dapper.ExecuteStoredProcedure<CustomizeReportResponseDTO>(
            "sp_GetAllReports", null);
        return result.ToList();
    }

    public async Task<DownloadReportResponseDTO?> DownloadReport(DownloadReportRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ReportId", request.ReportId);
        parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);

        return await _dapper.ExecuteStoredProcedureSingle<DownloadReportResponseDTO>(
            "sp_DownloadReport", parameters);
    }

     public async Task<UploadReportResponseDTO?> UploadReport(UploadReportRequestDTO request, string reportPath)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ReportId", request.ReportId);
        parameters.Add("@CompanyId", request.CompanyId, dbType: DbType.Int64, direction: ParameterDirection.Input);
        parameters.Add("@ReportPath", reportPath);
        parameters.Add("@ReportPathId", dbType: DbType.Int64, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedure<UploadReportResponseDTO>(
            "sp_UploadReport", parameters);

        var reportPathId = parameters.Get<long>("@ReportPathId");

        if (reportPathId == -1)
        {
            return null; // Indicates upload not allowed
        }

        return new UploadReportResponseDTO
        {
            ReportPathId = reportPathId,
            ReportPath = reportPath
        };
    }
}
