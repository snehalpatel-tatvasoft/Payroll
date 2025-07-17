using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings;

public interface ICustomizeReportService
{
    Task<JsonResult> GetAllReports(CustomizeReportRequestDTO request);
    Task<JsonResult> DownloadReport(DownloadReportRequestDTO request);
    // Task<JsonResult> UploadReport(UploadReportRequestDTO request);

    Task<JsonResult> UploadReport(UploadReportRequestDTO request, string fileName);
}