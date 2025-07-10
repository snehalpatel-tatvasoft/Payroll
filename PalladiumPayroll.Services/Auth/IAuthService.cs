using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;

namespace PalladiumPayroll.Services.Auth
{
    public interface IAuthService
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
    }
}
