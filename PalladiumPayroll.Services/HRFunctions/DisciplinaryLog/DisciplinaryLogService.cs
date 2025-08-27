using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Repositories.HRFunctions.DisciplinaryLog;
using PalladiumPayroll.Services.HRFunctions.DisciplinaryLog;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.DisciplinaryLog
{
    public class DisciplinaryLogService : IDisciplinaryLogService
    {
        private readonly IDisciplinaryLogRepository _disciplinaryLogRepository;
        private readonly DirectoryPathSetting _directoryPathSetting;

        public DisciplinaryLogService(IDisciplinaryLogRepository disciplinaryLogRepository, AppSettingPathHelper directoryPathSetting)
        {
            _disciplinaryLogRepository = disciplinaryLogRepository;
            _directoryPathSetting = directoryPathSetting.GetAppSettingDirectoryPath();
        }

        public async Task<JsonResult> GetDisciplinaryLogByCompanyId(long companyId)
        {

            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            var logs = await _disciplinaryLogRepository.GetDisciplinaryLogByCompanyId(companyId);
            if (logs.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(logs, ResponseMessages.DataFetchSuccess);
            }
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.DisciplinaryLog);

        }

        public async Task<JsonResult> GetEmployeesForDisciplinaryLogDropdown(long companyId)
        {

            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            var employees = await _disciplinaryLogRepository.GetEmployeesForDisciplinaryLogDropdown(companyId);
            if (employees.Any())
            {
                return HttpStatusCodeResponse.SuccessResponse(employees, ResponseMessages.DataFetchSuccess);
            }
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.Employee);
        }


        public async Task<JsonResult> UpsertDisciplinaryLog(DisciplinaryLogUpsertDTO request)
        {
            if (request.File != null && request.File.Length > 0)
            {
                var basePath = _directoryPathSetting.DisplinaryLogDocument;
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

            bool isSaved = await _disciplinaryLogRepository.UpsertDisciplinaryLog(request);

            if (!isSaved)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DisciplinaryLogSaveFailed);
            }

            return HttpStatusCodeResponse.SuccessResponse(
                string.Empty,
                string.Format(ResponseMessages.Success, ResponseMessages.DisciplinaryLog, ActionType.Saved)
            );
        }


        public async Task<JsonResult> GetDisciplinaryLogById(long disciplinaryLogId)
        {
            if (disciplinaryLogId <= 0)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            var log = await _disciplinaryLogRepository.GetDisciplinaryLogById(disciplinaryLogId);
            if (log != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(log, ResponseMessages.DataFetchSuccess);
            }
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.DisciplinaryLog);
        }


        public async Task<JsonResult> DeleteDisciplinaryLog(long disciplinaryLogId)
        {
            if (disciplinaryLogId <= 0)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }

            var rowsAffected = await _disciplinaryLogRepository.DeleteDisciplinaryLog(disciplinaryLogId);
            if (rowsAffected > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse(true, string.Format(ResponseMessages.Success, ResponseMessages.DisciplinaryLog, ActionType.Deleted));
            }
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.DisciplinaryLog);
        }


        public async Task<byte[]> DownloadDisciplinaryLogDocument(string documentUrl)
        {
            var basePath = _directoryPathSetting.DisplinaryLogDocument;
            var fullPath = FileHandler.CombinePath(basePath, documentUrl);

            return await FileHandler.ReadFileBytes(fullPath);
        }
        

        public async Task<JsonResult> DeleteDisciplinaryLogDocument(string documentUrl)
        {
            string basePath = _directoryPathSetting.DisplinaryLogDocument
                .Replace("/", Path.DirectorySeparatorChar.ToString());

            var filePath = FileHandler.CombinePath(basePath, documentUrl);

            bool isDeleted = FileHandler.DeleteFile(filePath);

            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    string.Empty,
                    string.Format(ResponseMessages.Success, "Document", ActionType.Deleted)
                );
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }
}