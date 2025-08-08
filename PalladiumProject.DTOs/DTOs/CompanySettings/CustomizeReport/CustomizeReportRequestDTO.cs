using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings
{
    public class CustomizeReportRequestDTO
    {
        public int? CompanyId { get; set; }
    }

    public class DownloadReportRequestDTO
    {
        public int ReportId { get; set; }
        public int? CompanyId { get; set; }
    }

    public class UploadReportRequestDTO
    {
        public int ReportId { get; set; }
        public int? CompanyId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}


