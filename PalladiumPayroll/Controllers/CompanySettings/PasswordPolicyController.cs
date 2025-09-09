using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordPolicyController : ControllerBase
    {
        private readonly IPasswordPolicyService _passwordPolicyService;

        public PasswordPolicyController(IPasswordPolicyService passwordPolicyService)
        {
            _passwordPolicyService = passwordPolicyService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetPasswordPolicyByCompanyId(long companyId)
        {
            try
            {
                if (companyId <= 0)
                {
                    return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
                }
                return await _passwordPolicyService.GetPasswordPolicyByCompanyId(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.PasswordPolicy));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreatePasswordPolicy([FromBody] PasswordPolicyRequestDTO request)
        {
            try
            {
                return await _passwordPolicyService.CreatePasswordPolicy(request);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
               string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.PasswordPolicy)
           );
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdatePasswordPolicy([FromBody] PasswordPolicyRequestDTO request, long passwordPolicyId)
        {
            try
            {
                return await _passwordPolicyService.UpdatePasswordPolicy(request, passwordPolicyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.PasswordPolicy));
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeletePasswordPolicy(long passwordPolicyId, long companyId)
        {
            try
            {
                if (passwordPolicyId <= 0 || companyId <= 0)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse("Invalid companyId or PasswordId.");
                }
                return await _passwordPolicyService.DeletePasswordPolicy(passwordPolicyId, companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(
               string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.PasswordPolicy)
           );
            }
        }
    }
}