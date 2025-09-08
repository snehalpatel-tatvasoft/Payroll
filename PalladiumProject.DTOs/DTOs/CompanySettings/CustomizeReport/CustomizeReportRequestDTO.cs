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
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}


