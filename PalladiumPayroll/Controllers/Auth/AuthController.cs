using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
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

        [HttpPost("[action]")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                return await _authService.Login(loginRequest);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> LoginSelectedUser(string userId)
        {
            try
            {
                return await _authService.LoginSelectedUser(userId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> ForgotPassWord(string email)
        {
            try
            {
                return await _authService.ForgotPassWord(email);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> ForgotPassWordSelectedUser(string userId)
        {
            try
            {
                return await _authService.ForgotPassWordSelectedUser(userId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequest requestData)
        {
            try
            {
                return await _authService.ResetPassword(requestData);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpPost("[action]")]
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

        [HttpPost("[action]")]
        public async Task<ActionResult> ConfirmEmail(string userId)
        {
            try
            {
                await _userService.ConfirmEmail(userId);

                return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.EmailVerified);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CheckIsUserLoggedIn(string userId)
        {
            try
            {
                bool res = await _userService.CheckIsUserLoggedIn(userId);
                if (!res)
                {
                    return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.LoggedOutDueToInActivity);
                }
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCompaniesByEmail(string email)
        {
            try
            {
                List<CompanyDetails>? companies = await _userService.GetCompaniesByEmail(email);
                return HttpStatusCodeResponse.SuccessResponse(companies, "");
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Company, ex.Message));
            }
        }

        [HttpPost("[action]")]
        public IActionResult RefreshToken(RefreshRequest request)
        {
            try
            {
                return _authService.RefreshRequest(request);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.InternalServerError);
            }
        }
    }
}
