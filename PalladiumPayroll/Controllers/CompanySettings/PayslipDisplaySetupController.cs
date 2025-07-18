using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;


[ApiController]
[Route("api/[controller]")]
public class PayslipDisplaySetupController : ControllerBase
{
    private readonly IPayslipDisplaySetupService _payslipDisplaySetupService;

    public PayslipDisplaySetupController(IPayslipDisplaySetupService payslipDisplaySetupService)
    {
        _payslipDisplaySetupService = payslipDisplaySetupService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> SavePayslipDisplaySettings(SavePayslipSettingsDTO request)
    {
        try
        {
            if (request.CompanyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }
            return await _payslipDisplaySetupService.SavePayslipDisplaySettings(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.PayslipDisplaySetup, ex.Message)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetPayslipSettings(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _payslipDisplaySetupService.GetPayslipSettingsByCompanyId(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.PayslipDisplaySetup, ex.Message)
            );
        }
    }
}
