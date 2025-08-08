using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

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

        [HttpGet("GetPayrollCycles/{companyId}")]
        public async Task<ActionResult> GetPayrollCycles(long companyId)
        {
            try
            {
                JsonResult? res = await _timesheetSetupService.GetPayrollCycles(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetTimesheetPayrollSetup/{companyId}")]
        public async Task<ActionResult> GetTimesheetPayrollSetup(long companyId)
        {
            try
            {
                JsonResult? res = await _timesheetSetupService.GetTimesheetPayrollSetup(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPost("UpsertTimesheetPayrollSetup")]
        public async Task<ActionResult> UpsertTimesheetPayrollSetup([FromBody] TimesheetSetupRequestDTO request)
        {
            try
            {
                JsonResult? res = await _timesheetSetupService.UpsertTimesheetPayrollSetup(request);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}