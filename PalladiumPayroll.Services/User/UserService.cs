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
    }
}
