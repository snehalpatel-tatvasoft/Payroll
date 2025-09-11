using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings;

public interface ICustomizeReportService
{
    Task<JsonResult> GetAllReports();
    Task<JsonResult> DownloadReport(DownloadReportRequestDTO request);
    Task<JsonResult> UploadReport(UploadReportRequestDTO request);

} 