using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Services.Auth
{
    public interface IAuthService
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
    }
}
