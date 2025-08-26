using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTraining;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.EmployeeTraining;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTrainingController : ControllerBase
{
    private readonly IEmployeeTrainingService _employeeTrainingService;

    public EmployeeTrainingController(IEmployeeTrainingService employeeTrainingService)
    {
        _employeeTrainingService = employeeTrainingService;
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> UpsertEmployeeTraining([FromForm] EmployeeTrainingUpsertData request)
    {
        try
        {
            if (request.EmployeeTrainingId < 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeTrainingnId);
            }
            return await _employeeTrainingService.UpsertEmployeeTraining(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.EmployeeTraining)
            );
        }
    }


    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeeTraining(long employeeTrainingId)
    {
        try
        {
            if (employeeTrainingId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeTrainingnId);
            }

            return await _employeeTrainingService.DeleteEmployeeTraining(employeeTrainingId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeeTraining)
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeTraining));
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeTraining));
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeTraining));
        }
    }

    [HttpPost("[action]")]
    public async Task<JsonResult> AddEmployeeTrainingDropdownItem(EmployeeTrainingDropdownItem reqItem)
    {
        try
        {
            return await _employeeTrainingService.AddEmployeeTrainingDropdownItem(reqItem);
        }
        catch (Exception)
        {
             return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.EmployeeTraining+" droplist item"));
        }
    }

    [HttpDelete("[action]")]
    public async Task<JsonResult> DeleteEmployeeTrainingDropdownItem(int id, int type)
    {
        try
        {
            return await _employeeTrainingService.DeleteEmployeeTrainingDropdownItem(id, type);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeeTraining+" droplist item"));
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> DownloadDocument(string fileUrl)
    {
        try
        {
            return File(await _employeeTrainingService.DownloadDocument(fileUrl), "application/octet-stream", fileUrl.Split("\\").LastOrDefault());
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, "downloading", ResponseMessages.EmployeeTraining+" documnet"));
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteDocument([FromQuery] TrainingDocumentDelete reqModel)
    {
        try
        {
            return await _employeeTrainingService.DeleteTrainingDocument(reqModel.DocumentUrl);
        }
        catch (Exception)
        {
           return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeeTraining+" documnet."));
        }
    }
}
