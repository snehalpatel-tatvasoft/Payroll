using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.User
{
    public interface IUserService
    {
        Task<UserResponse> GetUserInfo(string email);
        Task<bool> ConfirmEmail(string userId);
        Task<bool> CheckIsUserLoggedIn(string userId);
        Task<bool> UpdateLastActivity(string userId);
        Task<bool> LogoutInactiveUsers();
    }
}
