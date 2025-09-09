using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.User
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetUserInfo(string email);
        Task<bool> ConfirmEmail(string userId);
        Task<bool> CheckIsUserLoggedIn(string userId);
        Task<bool> UpdateLastActivity(string userId);
        Task<bool> LogoutInactiveUsers();
        Task<List<CompanyDetails>> GetCompaniesByEmail(string email);
        Task<JsonResult> ChangeEmail(ChangeEmailModel reqModel);
        Task<JsonResult> ChangePassword(ChangePasswordModel reqModel);
    }
}
