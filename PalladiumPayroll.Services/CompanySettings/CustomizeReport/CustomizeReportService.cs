using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.Helper;
using static PalladiumPayroll.Helper.Constants.AppEnums;


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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                    string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CustomizeReport));
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                               string.Format(ResponseMessages.ExceptionMessage, "Downloading", ResponseMessages.CustomizeReport));
        }

    }

    public async Task<JsonResult> UploadReport(UploadReportRequestDTO request)
    {
        try
        {
            if (request.File != null && request.File.Length > 0)
            {
                var basePath = _directoryPathSetting.CustomizeReportDocument;
                var finalPath = FileHandler.CombinePath(basePath, "");

                FileHandler.CreateDirectory(finalPath);

                var filePath = Path.Combine(finalPath, request.File.FileName);
                FileHandler.DeleteFile(filePath);
                await FileHandler.UploadFile(filePath, request.File);

                var relativePath = Path.Combine(basePath, request.File.FileName).Replace("\\", "/");

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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                               string.Format(ResponseMessages.ExceptionMessage, "Uploading", ResponseMessages.CustomizeReport));
        }

    }

}