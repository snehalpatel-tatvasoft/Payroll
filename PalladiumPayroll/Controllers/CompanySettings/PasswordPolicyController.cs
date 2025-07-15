using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

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

        [HttpGet("GetPasswordPolicyByCompanyId/{companyId}")]
        public async Task<ActionResult> GetPasswordPolicyByCompanyId(long companyId)
        {
            try
            {
                JsonResult? res = await _passwordPolicyService.GetPasswordPolicyByCompanyId(companyId);
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

        [HttpPost("CreatePasswordPolicy")]
        public async Task<ActionResult> CreatePasswordPolicy([FromBody] PasswordPolicyRequestDTO request)
        {
            try
            {
                JsonResult? res = await _passwordPolicyService.CreatePasswordPolicy(request);
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

        [HttpPut("UpdatePasswordPolicy/{passwordPolicyId}")]
        public async Task<ActionResult> UpdatePasswordPolicy([FromBody] PasswordPolicyRequestDTO request, long passwordPolicyId)
        {
            try
            {
                JsonResult? res = await _passwordPolicyService.UpdatePasswordPolicy(request, passwordPolicyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = request,

                });
            }
        }

        [HttpDelete("DeletePasswordPolicy/{passwordPolicyId}/{companyId}")]
        public async Task<ActionResult> DeletePasswordPolicy(long passwordPolicyId, long companyId)
        {
            try
            {
                JsonResult? res = await _passwordPolicyService.DeletePasswordPolicy(passwordPolicyId, companyId);
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