using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;

namespace PalladiumPayroll.Repositories.CompanySettings;

public interface ICustomizeReportRepository
{
    Task<List<CustomizeReportResponseDTO>> GetAllReports(CustomizeReportRequestDTO request);
    Task<DownloadReportResponseDTO?> DownloadReport(DownloadReportRequestDTO request);
    Task<UploadReportResponseDTO?> UploadReport(UploadReportRequestDTO request, string reportPath);
}