using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.EmployeeTraining;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTrainingController:ControllerBase
{
     private readonly IEmployeeTrainingService _employeeTrainingService;

    public EmployeeTrainingController(IEmployeeTrainingService employeeTrainingService)
    {
        _employeeTrainingService = employeeTrainingService;
    }


     [HttpPost("[action]")]
    public async Task<ActionResult> UpsertEmployeeTraining(EmployeeTrainingUpsertData request)
    {
        try
        {
            if (request.EmployeeTrainingId < 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeTrainingnId);
            }
            return await _employeeTrainingService.UpsertEmployeeTraining(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeeTraining, ex.Message)
            );
        }
    }

    
    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeeTraining(long employeeTrainingId, string userId)
    {
        try
        {
            if (employeeTrainingId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeTrainingnId);
            }

            return await _employeeTrainingService.DeleteEmployeeTraining(employeeTrainingId, userId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.EmployeeTraining, ex.Message)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeTrainingDropdownData(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeTrainingService.GetEmployeeTrainingDropdownData(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTraining, ex.Message));
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeTrainings(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeTrainingService.GetEmployeeTrainings(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTraining, ex.Message));
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeTrainingById(long trainingId)
    {
        try
        {
            if (trainingId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            }
            return await _employeeTrainingService.GetEmployeeTrainingById(trainingId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTraining, ex.Message));
        }
    }
}
