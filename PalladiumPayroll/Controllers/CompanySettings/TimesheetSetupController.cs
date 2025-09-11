using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetSetupController : ControllerBase
    {
        private readonly ITimesheetSetupService _timesheetSetupService;

        public TimesheetSetupController(ITimesheetSetupService timesheetSetupService)
        {
            _timesheetSetupService = timesheetSetupService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPayrollCycles(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _timesheetSetupService.GetPayrollCycles(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.PayrollCycle));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTimesheetPayrollSetup(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _timesheetSetupService.GetTimesheetPayrollSetup(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.TimesheetSetup));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertTimesheetPayrollSetup([FromBody] TimesheetSetupRequestDTO request)
        {
            try
            {
                if (request.CompanyId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _timesheetSetupService.UpsertTimesheetPayrollSetup(request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
                   string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.TimesheetSetup)
                );
            }
        }
    }
}