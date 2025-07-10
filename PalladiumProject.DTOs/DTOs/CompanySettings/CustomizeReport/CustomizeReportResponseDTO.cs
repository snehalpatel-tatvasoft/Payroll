namespace PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings
{
    public class CustomizeReportResponseDTO
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; } = null!;
    }

    public class DownloadReportResponseDTO
    {
        public string ReportPath { get; set; } = null!;
    }

    public class UploadReportResponseDTO
    {
        public long ReportPathId { get; set; }
        public string ReportPath { get; set; } = null!;
    }


}