using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings.EmployeeProfile;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class EmployeeProfileController : ControllerBase
{
    private readonly IEmployeeProfileService _employeeProfileService;

    public EmployeeProfileController(IEmployeeProfileService employeeProfileService)
    {
        _employeeProfileService = employeeProfileService;
    }

    #region Profile

    [HttpPost("[action]")]
    public async Task<ActionResult> CreateProfile([FromBody] EmployeeProfileRequestDTO request)
    {
        try
        {
            JsonResult? res = await _employeeProfileService.CreateProfile(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.Designations));
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetAllEmployeeProfiles(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeProfileService.GetAllEmployeeProfiles(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeeProfile));
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeeProfile(long profileId)
    {
        try
        {
            return await _employeeProfileService.DeleteEmployeeProfile(profileId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeeProfile));
        }
    }

    #endregion


    #region work information

    [HttpGet("[action]")]
    public async Task<ActionResult> GetWorkInformatiionDropdownData(int companyId)
    {
        try
        {
            return await _employeeProfileService.GetWorkInformatiionDropdownData(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> SaveWorkInformation([FromBody] WorkInformationRequestDTO request)
    {
        try
        {
            JsonResult? res = await _employeeProfileService.SaveWorkInformation(request);
            return res;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, "Work Information"));
        }
    }

    #endregion


    #region Leave Settings

    [HttpGet("[action]")]
    public async Task<ActionResult> GetLeaveRulesForEmployeeProfile(int companyId, int caseId, long profileId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            if (profileId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Invalid Profile Id.");
            }

            if (caseId < 1 || caseId > 8)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Invalid Case ID.");
            }

            return await _employeeProfileService.GetLeaveRulesForEmployeeProfile(companyId, caseId, profileId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.LeaveSettings));
        }
    }


    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateLeaveRulesInEmployeeProfile(LeaveSettingsUpdateRequestDTO request)
    {
        try
        {
            return await _employeeProfileService.UpdateLeaveSettingsInEmployeeProfile(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.LeaveSettings)
            );
        }
    }

    #endregion

    #region Transaction
    [HttpGet("[action]")]
    public async Task<ActionResult> GetModalTransactionsList(int transactionId)
    {
        try
        {
            return await _employeeProfileService.GetModalTransactionsList(transactionId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Transaction list"));
        }
    }
    [HttpGet("[action]")]
    public async Task<ActionResult> GetTransactionsList(int transactionId , int companyId,int profileId)
    {
        try
        {
            return await _employeeProfileService.GetTransactionsList(transactionId,companyId,profileId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Transaction list"));
        }
    }
    [HttpPost("[action]")]
    public async Task<ActionResult> SaveTransactionAssignments([FromBody] SaveTransactionAssignmentsRequestDTO request)
    {
        try
        {
            if (request.CompanyId <= 0 || request.ProfileId <= 0 || request.PayrollProcessId == null || !request.PayrollProcessId.Any())
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid request data.");
            }

            return await _employeeProfileService.SaveTransactionAssignments(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, "Transaction Assignments"));
        }
    }

    #endregion

}
