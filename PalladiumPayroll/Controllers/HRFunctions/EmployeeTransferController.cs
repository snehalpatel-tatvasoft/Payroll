using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTransfer;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.EmployeeTransfer;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class EmployeeTransferController : ControllerBase
{
    private readonly IEmployeeTransferService _employeeTransferService;

    public EmployeeTransferController(IEmployeeTransferService employeeTransferService)
    {
        _employeeTransferService = employeeTransferService;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeTransferDropdownData(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            var response = await _employeeTransferService.GetEmployeeTransferDropdownData(companyId);
            return HttpStatusCodeResponse.SuccessResponse(response, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTransfer, ActionType.Retrieved));
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTransfer, ex.Message));
        }
    }

    [HttpGet("[action]")]
    public async Task<JsonResult> GetEmployeeAutoFillData(long employeeId, long companyId)
    {
        try
        {
            return await _employeeTransferService.GetEmployeeAutoFillData(employeeId, companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTransfer, ex.Message));
        }
    }

    [HttpPost("AddEmployeeTransfer")]
    public async Task<ActionResult> AddEmployeeTransfer([FromBody] EmployeeTransferRequestDTO request)
    {
        try
        {
            var res = await _employeeTransferService.AddEmployeeTransfer(request);
            return res;
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeeTransfer, ex.Message));
        }
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeTransferList(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            var response = await _employeeTransferService.GetEmployeeTransferList(companyId);
            return HttpStatusCodeResponse.SuccessResponse(response, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTransfer, ActionType.Retrieved));
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTransfer, ex.Message));
        }
    }


}