using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.User
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExist(string email);
        Task<List<UserResponse>> GetUserInfo(string email);
        Task<bool> ResetPassword(string userId, string password);
        Task<UserResponse?> GetUserInfoByUserId(string userId);
        Task<bool> ConfirmEmail(string userId);
        Task<bool> CheckIsUserLoggedIn(string userId);
        Task<bool> UpdateLastActivity(string userId);
        Task<bool> LoginUser(string userId);
        Task<bool> LogoutInactiveUsers();
        Task<List<CompanyDetails>> GetCompaniesByEmail(string email);
        Task<int> UpdateUserEmail(string email, string newEmail);
        Task<string?> GetUserPassword();
        Task<bool> UpdateUserPassword(string newPassword);
    }
}
