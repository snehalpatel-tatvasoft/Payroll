using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using PalladiumPayroll.Repositories.Auth;

namespace PalladiumPayroll.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<JsonResult> Login(LoginRequest loginRequest)
        {
            return await _authRepository.Login(loginRequest);
        }
    }
}
