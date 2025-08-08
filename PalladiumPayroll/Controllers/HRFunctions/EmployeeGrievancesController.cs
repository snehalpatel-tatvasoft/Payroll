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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeeGrievances, ex.Message)
            );
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeeGrievance(long employeeGrievanceId, string userId)
    {
        try
        {
            if (employeeGrievanceId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeeGrievanceId);
            }

            return await _employeeGrievancesService.DeleteEmployeeGrievance(employeeGrievanceId, userId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.EmployeeGrievances, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.NatureOfGrievances, ex.Message));
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeGrievances, ex.Message));
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeGrievances, ex.Message));
        }
    }

}
