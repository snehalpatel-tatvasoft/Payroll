using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Auth;
using PalladiumPayroll.Services.Company;
using System.Net;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICompanyService _companyService;
        public AuthController(IAuthService authService, ICompanyService companyService)
        {
            _authService = authService;
            _companyService = companyService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                JsonResult? loginResponse = await _authService.Login(loginRequest);
                return loginResponse;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateCompany")]
        public async Task<ActionResult> CreateCompany(CreateCompanyRequest request)
        {
            try
            {
                JsonResult? res = await _companyService.CreateCompany(request);
                return res;
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }
    }
}
