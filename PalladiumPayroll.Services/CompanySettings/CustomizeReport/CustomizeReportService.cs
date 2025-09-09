using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.Helper;
using static PalladiumPayroll.Helper.Constants.AppEnums;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;


namespace PalladiumPayroll.Services.Company_Settings;

public class CustomizeReportService : ICustomizeReportService
{
    private readonly ICustomizeReportRepository _customizeReportRepository;
    private readonly DirectoryPathSetting _directoryPathSetting;
    public CustomizeReportService(ICustomizeReportRepository customizeReportRepository, AppSettingPathHelper directoryPathSetting)
    {
        _customizeReportRepository = customizeReportRepository;
        _directoryPathSetting = directoryPathSetting.GetAppSettingDirectoryPath();

    }

    public async Task<JsonResult> GetAllReports()
    {
        List<CustomizeReportResponseDTO> reports = await _customizeReportRepository.GetAllReports();

        return HttpStatusCodeResponse.SuccessResponse(reports, ResponseMessages.DataFetchSuccess);

    }

    public async Task<JsonResult> DownloadReport(DownloadReportRequestDTO request)
    {
        DownloadReportResponseDTO? reportPath = await _customizeReportRepository.DownloadReport(request);
        if (reportPath != null)
        {
            return HttpStatusCodeResponse.SuccessResponse(reportPath, ResponseMessages.DataFetchSuccess);
        }
        return HttpStatusCodeResponse.NotFoundResponse("Report Path");
    }

    public async Task<JsonResult> UploadReport(UploadReportRequestDTO request)
    {
        if (request.File != null && request.File.Length > 0)
        {
            string? basePath = _directoryPathSetting.CustomizeReportDocument;
            string? finalPath = FileHandler.CombinePath(basePath, "");

            FileHandler.CreateDirectory(finalPath);

            string? filePath = Path.Combine(finalPath, request.File.FileName);
            FileHandler.DeleteFile(filePath);
            await FileHandler.UploadFile(filePath, request.File);

            string? relativePath = Path.Combine(basePath, request.File.FileName).Replace("\\", "/");

            request.FileName = request.File.FileName;
            request.FilePath = relativePath;
            request.FileSize = request.File.Length;
            request.FileType = request.File.ContentType;
        }

        bool isSaved = await _customizeReportRepository.UploadReport(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.ErrorSavingCustomizeReport);
        }

        return HttpStatusCodeResponse.SuccessResponse(
            string.Empty,
            string.Format(ResponseMessages.Success, ResponseMessages.CustomizeReport, ActionType.Saved)
        );
    }

}