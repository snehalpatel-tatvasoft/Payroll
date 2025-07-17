using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class EmployeeCodesController : ControllerBase
{
    private readonly IEmployeeCodesService _employeeCodesService;

    public EmployeeCodesController(IEmployeeCodesService employeeCodesService)
    {
        _employeeCodesService = employeeCodesService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> SaveEmployeeCodeGeneration(EmployeeCodeRequestDTO request)
    {
        try
        {
            if (request.CompanyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }
            return await _employeeCodesService.SaveEmployeeCodeGeneration(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeeCode, ex.Message)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeeCode(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeeCodesService.GetEmployeeCodeByCompanyId(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeCode, ex.Message)
            );
        }
    }
}
