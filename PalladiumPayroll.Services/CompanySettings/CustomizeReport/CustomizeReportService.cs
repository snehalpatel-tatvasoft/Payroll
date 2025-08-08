using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.Services.CompanySettings;

namespace PalladiumPayroll.Services.Company_Settings;

public class CustomizeReportService : ICustomizeReportService
{
    private readonly ICustomizeReportRepository _customizeReportRepository;

    public CustomizeReportService(ICustomizeReportRepository customizeReportRepository)
    {
        _customizeReportRepository = customizeReportRepository;
    }

    public async Task<JsonResult> GetAllReports(CustomizeReportRequestDTO request)
    {
        try
        {
            var reports = await _customizeReportRepository.GetAllReports(request);
            if (reports.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(reports, ResponseMessages.DataFetchSuccess);
            }
            return HttpStatusCodeResponse.NotFoundResponse("Reports");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse($"Error fetching reports: {ex.Message}");
        }
    }

    public async Task<JsonResult> DownloadReport(DownloadReportRequestDTO request)
    {
        try
        {
            var reportPath = await _customizeReportRepository.DownloadReport(request);
            if (reportPath != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(reportPath, ResponseMessages.DataFetchSuccess);
            }
            return HttpStatusCodeResponse.NotFoundResponse("Report Path");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse($"Error downloading report: {ex.Message}");
        }
    }

    public async Task<JsonResult> UploadReport(UploadReportRequestDTO request, string fileName)
    {
        try
        {
            if (request.File == null || request.File.Length == 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("No file uploaded.");
            }

            if (!request.File.FileName.EndsWith(".repx", StringComparison.OrdinalIgnoreCase))
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Only .repx files are allowed.");
            }

            var relativePath = $"uploaded-reports/{fileName}";

            var response = await _customizeReportRepository.UploadReport(request, relativePath);

            if (response == null)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Upload not allowed: ReportId {request.ReportId} does not have default file available");
            }

            return HttpStatusCodeResponse.SuccessResponse(response, "Report uploaded successfully.");
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse($"Failed to upload report: {ex.Message}");
        }
    }
}