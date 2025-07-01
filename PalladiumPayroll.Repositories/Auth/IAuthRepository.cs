using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;

namespace PalladiumPayroll.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
    }
}
