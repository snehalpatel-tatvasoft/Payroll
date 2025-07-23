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
            List<EmployeeTrainingDisplayDataDTO> employeeTrainings= await _employeeTrainingRepository.GetEmployeeTrainingDisplayData(companyId);

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

}
