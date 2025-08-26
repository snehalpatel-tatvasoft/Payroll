using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeTraining;

public class EmployeeTrainingService : IEmployeeTrainingService
{
    private readonly IEmployeeTrainingRepository _employeeTrainingRepository;
    private readonly DirectoryPathSetting _directoryPathSetting;

    public EmployeeTrainingService(IEmployeeTrainingRepository employeeTrainingRepository, AppSettingPathHelper directoryPathSetting)
    {
        _employeeTrainingRepository = employeeTrainingRepository;
        _directoryPathSetting = directoryPathSetting.GetAppSettingDirectoryPath();
    }

    public async Task<JsonResult> UpsertEmployeeTraining(EmployeeTrainingUpsertData request)
    {
        if (request.File != null && request.File.Length > 0)
        {
            var basePath = _directoryPathSetting.TrainingDocument;
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

        bool isSaved = await _employeeTrainingRepository.UpsertEmployeeTraining(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmployeeTrainingSaveFailed);
        }

        return HttpStatusCodeResponse.SuccessResponse(
            string.Empty,
            string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Saved)
        );
    }


    public async Task<JsonResult> DeleteEmployeeTraining(long employeeTrainingId)
    {
        bool isDeleted = await _employeeTrainingRepository.DeleteEmployeeTraining(employeeTrainingId);

        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeTrainingNotFound);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Deleted));
    }

    public async Task<JsonResult> GetEmployeeTrainingDropdownData(long companyId)
    {
        EmployeeTrainingDropdownsDTO? data = await _employeeTrainingRepository.GetEmployeeTrainingDropdownData(companyId);

        return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeeTrainings(long companyId)
    {
        List<EmployeeTrainingDisplayDataDTO> employeeTrainings = await _employeeTrainingRepository.GetEmployeeTrainingDisplayData(companyId);

        return HttpStatusCodeResponse.SuccessResponse(employeeTrainings, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Retrieved));
    }


    public async Task<JsonResult> GetEmployeeTrainingById(long trainingId)
    {
        EmployeeTrainingDetailDTO? employeeTraining = await _employeeTrainingRepository.GetEmployeeTrainingById(trainingId);

        return HttpStatusCodeResponse.SuccessResponse(employeeTraining, string.Empty);
    }

    public async Task<byte[]> DownloadDocument(string documentUrl)
    {
        var basePath = _directoryPathSetting.TrainingDocument;
        var fullPath = FileHandler.CombinePath(basePath, documentUrl);

        return await FileHandler.ReadFileBytes(fullPath);
    }

    public async Task<JsonResult> DeleteTrainingDocument(string documentUrl)
    {
        string basePath = _directoryPathSetting.TrainingDocument
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


    public async Task<JsonResult> AddEmployeeTrainingDropdownItem(EmployeeTrainingDropdownItem reqItem)
    {
        List<DropDownViewModel> result = await _employeeTrainingRepository.AddEmployeeTrainingDropdownItem(reqItem);
        if (result.Count > 0 && result.FirstOrDefault()?.Id > 0)
        {
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Item", ActionType.Saved));
        }
        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
    }

    public async Task<JsonResult> DeleteEmployeeTrainingDropdownItem(int id, int type)
    {
        bool result = await _employeeTrainingRepository.DeleteEmployeeTrainingDropdownItem(id, type);
        if (result)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Item", ActionType.Deleted));
        }
        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
    }
}
