using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;

namespace PalladiumPayroll.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
        Task<JsonResult> LoginSelectedUser(string userId);
        Task<JsonResult> ForgotPassWord(string email);
        Task<JsonResult> ForgotPasswordSelectedUser(string userId);
        Task<JsonResult> ResetPassword(ResetPasswordRequest requestData);
        JsonResult RefreshRequest(RefreshRequest request);
    }
}
