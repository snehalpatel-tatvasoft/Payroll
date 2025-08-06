using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.HRFunctions.EmployeeTraining;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeTraining;

public class EmployeeTrainingService : IEmployeeTrainingService
{
    private readonly IEmployeeTrainingRepository _employeeTrainingRepository;
    private readonly string _fileUploadPath = @"E:\PCTR25\Payroll-final-Project\PremiumPayProject-Frontend\Payroll-UI\src\assets\employee-training-documents\";

    public EmployeeTrainingService(IEmployeeTrainingRepository employeeTrainingRepository)
    {
        _employeeTrainingRepository = employeeTrainingRepository;
    }

    public async Task<JsonResult> UpsertEmployeeTraining(EmployeeTrainingUpsertData request)
    {
        try
        {
            bool isSaved = await _employeeTrainingRepository.UpsertEmployeeTraining(request);

            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeTrainingSaveFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Saved));

        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> DeleteEmployeeTraining(long employeeTrainingId, string userId)
    {
        try
        {
            bool isDeleted = await _employeeTrainingRepository.DeleteEmployeeTraining(employeeTrainingId, userId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeTrainingNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Deleted));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeeTrainingDropdownData(long companyId)
    {
        try
        {
            EmployeeTrainingDropdownsDTO? data = await _employeeTrainingRepository.GetEmployeeTrainingDropdownData(companyId);

            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeeTrainings(long companyId)
    {
        try
        {
            List<EmployeeTrainingDisplayDataDTO> employeeTrainings = await _employeeTrainingRepository.GetEmployeeTrainingDisplayData(companyId);

            return HttpStatusCodeResponse.SuccessResponse(employeeTrainings, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTraining, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> GetEmployeeTrainingById(long trainingId)
    {
        try
        {
            EmployeeTrainingDetailDTO? employeeTraining = await _employeeTrainingRepository.GetEmployeeTrainingById(trainingId);

            return HttpStatusCodeResponse.SuccessResponse(employeeTraining, string.Empty);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<string?> UploadTrainingFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        string[]? allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
        string? extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return null;

        if (file.Length > 2 * 1024 * 1024)
            return null;

        if (!Directory.Exists(_fileUploadPath))
            Directory.CreateDirectory(_fileUploadPath);

        string? originalFileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
        string? sanitizedFileName = string.Concat(originalFileNameWithoutExt.Split(Path.GetInvalidFileNameChars()));

        string? shortId = Guid.NewGuid().ToString("N")[..8];

        string? fileName = $"{sanitizedFileName}_{shortId}{extension}";
        string? fullPath = Path.Combine(_fileUploadPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        string? relativePath = $"assets/employee-training-documents/{fileName}";
        return relativePath;
    }


}
