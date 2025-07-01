using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.Repositories.User;

namespace PalladiumPayroll.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse> GetUserInfo(string email)
        {
            return await _userRepository.GetUserInfo(email);
        }

        public async Task<bool> ConfirmEmail(string userId)
        {
            return await _userRepository.ConfirmEmail(userId);
        }

        public async Task<bool> CheckIsUserLoggedIn(string userId)
        {
            return await _userRepository.CheckIsUserLoggedIn(userId);
        }

        public async Task<bool> UpdateLastActivity(string userId)
        {
            return await _userRepository.UpdateLastActivity(userId);
        }

        public async Task<bool> LogoutInactiveUsers()
        {
            return await _userRepository.LogoutInactiveUsers();
        } 
        
        public async Task<List<CompanyDetails>> GetCompaniesByEmail(string email)
        {
            return await _userRepository.GetCompaniesByEmail(email);
        }
    }
}
