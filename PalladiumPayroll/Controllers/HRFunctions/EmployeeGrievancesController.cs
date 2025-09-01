using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.EmployeeGrievances;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class EmployeeGrievancesController : ControllerBase
{
    private readonly IEmployeeGrievancesService _employeeGrievancesService;

    public EmployeeGrievancesController(IEmployeeGrievancesService employeeGrievancesService)
    {
        _employeeGrievancesService = employeeGrievancesService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UpsertEmployeeGrievance(EmployeeGrievanceUpsertData request)
    {
        try
        {
            if (request.EmployeeGrievanceId < 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeGrievanceId);
            }
            return await _employeeGrievancesService.UpsertEmployeeGrievance(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.EmployeeGrievances)
            );
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeeGrievance(long employeeGrievanceId)
    {
        try
        {
            if (employeeGrievanceId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeGrievanceId);
            }

            return await _employeeGrievancesService.DeleteEmployeeGrievance(employeeGrievanceId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeeGrievances)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeesForGrievances(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeGrievancesService.GetEmployeesForGrievances(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.Employee)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetNatureOfGrievances(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeGrievancesService.GetNatureOfGrievances(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.NatureOfGrievances)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeGrievances(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeGrievancesService.GetEmployeeGrievances(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeGrievances)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeGrievanceById(long grievanceId)
    {
        try
        {
            if (grievanceId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeGrievanceId);
            }

            return await _employeeGrievancesService.GetEmployeeGrievanceById(grievanceId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeGrievances)
            );
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> AddNatureOfGrievance([FromBody] NatureOfGrievancesDto  request)
    {
        try
        {
            if (request.CompanyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeGrievancesService.AddNatureOfGrievance(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.NatureOfGrievances)
            );
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteNatureOfGrievance(int natureOfGrievanceId)
    {
        try
        {
            if (natureOfGrievanceId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Invalid nature of grievances id.");
            }

            return await _employeeGrievancesService.DeleteNatureOfGrievance(natureOfGrievanceId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.NatureOfGrievances)
            );
        }
    }

}
