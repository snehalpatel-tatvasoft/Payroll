using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.User
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExist(string email);
        Task<List<UserResponse>> GetUserInfo(string email);
        Task<UserResponse?> GetUserInfoByUserId(string userId);
        Task<bool> ConfirmEmail(string userId);
        Task<bool> CheckIsUserLoggedIn(string userId);
        Task<bool> UpdateLastActivity(string userId);
        Task<bool> LoginUser(string userId);
        Task<bool> LogoutInactiveUsers();
        Task<List<CompanyDetails>> GetCompaniesByEmail(string email);
    }
}
