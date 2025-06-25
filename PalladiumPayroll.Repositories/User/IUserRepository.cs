using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.User
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExist(string email);
        Task<UserResponse?> GetUserInfo(string email);
        Task<bool> ConfirmEmail(string userId);
    }
}
