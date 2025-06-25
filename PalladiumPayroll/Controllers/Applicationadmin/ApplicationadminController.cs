using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper.License;
using PalladiumPayroll.Services.Applicationadmin;
using ServiceReference;
using System.ServiceModel;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.Applicationadmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationadminController : ControllerBase
    {
        private readonly IApplicationadminService _appAdminService;
        public ApplicationadminController(IApplicationadminService appAdminService)
        {
            _appAdminService = appAdminService;
        }

        [HttpGet("GetApplicationAdminDashboard")]
        public async Task<IActionResult> GetUserActivationData(int mode)
        {
            try
            {
                var res = await _appAdminService.GetUserActivationData(mode);
                if (res != null)
                {
                    return HttpStatusCodeResponse.SuccessResponse(res, string.Format(ResponseMessages.Success, ResponseMessages.AppAdminDashboard, ActionType.Retrieving));
                }

                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.AppAdminDashboard, null));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.AppAdminDashboard, ex.Message));
            }
        }
    }
}