using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;

namespace PalladiumPayroll.Services.Auth
{
    public interface IAuthService
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
        Task<JsonResult> LoginSelectedUser(string userId);
        Task<JsonResult> ForgotPassWord(string email);
        Task<JsonResult> ForgotPassWordSelectedUser(string userId);
        Task<JsonResult> ResetPassword(ResetPasswordRequest requestData);
        JsonResult RefreshRequest(RefreshRequest request);
    }
}
