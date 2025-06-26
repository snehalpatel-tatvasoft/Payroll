using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Auth;
using PalladiumPayroll.Services.Company;
using PalladiumPayroll.Services.User;
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
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, ICompanyService companyService, IUserService userService)
        {
            _authService = authService;
            _companyService = companyService;
            _userService = userService;
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

        [HttpPost("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId)
        {
            try
            {
                var res = await _userService.ConfirmEmail(userId);

                return HttpStatusCodeResponse.GenerateResponse(
                            result: true,
                            statusCode: HttpStatusCode.OK,
                            message: ResponseMessages.EmailVerified,
                            data: string.Empty
                        );
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }

        [HttpPost("CheckIsUserLoggedIn")]
        public async Task<ActionResult> CheckIsUserLoggedIn(string userId)
        {
            try
            {
                bool res = await _userService.CheckIsUserLoggedIn(userId);
                if (!res)
                {
                    return HttpStatusCodeResponse.GenerateResponse(result: false, statusCode: HttpStatusCode.OK, message: ResponseMessages.LoggedOutDueToInActivity, data: string.Empty);
                }
                return HttpStatusCodeResponse.GenerateResponse(result: true, statusCode: HttpStatusCode.OK, message: string.Empty, data: string.Empty);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }
    }
}
