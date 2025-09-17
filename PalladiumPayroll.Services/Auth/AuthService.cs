using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Auth;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            return await _authRepository.Login(loginRequest);
        }

        public async Task<JsonResult> LoginSelectedUser(string userId)
        {
            return await _authRepository.LoginSelectedUser(userId);
        }

        public async Task<JsonResult> ForgotPassWord(string email)
        {
            return await _authRepository.ForgotPassWord(email);
        }

        public async Task<JsonResult> ForgotPassWordSelectedUser(string userId)
        {
            return await _authRepository.ForgotPasswordSelectedUser(userId);
        }

        public async Task<JsonResult> ResetPassword(ResetPasswordRequest requestData)
        {
            return await _authRepository.ResetPassword(requestData);
        }

        public JsonResult RefreshRequest(RefreshRequest request)
        {
            return _authRepository.RefreshRequest(request);
        }
    }
}
