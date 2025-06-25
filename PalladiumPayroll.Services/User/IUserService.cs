using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.User
{
    public interface IUserService
    {
        Task<UserResponse> GetUserInfo(string email);

        Task<bool> ConfirmEmail(string userId);
    }
}
