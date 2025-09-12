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
    public async Task<ActionResult> GetTransactionsList(int transactionId)
    {
        try
        {
            return await _employeeProfileService.GetTransactionsList(transactionId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, "Transaction list"));
        }
    }
}
