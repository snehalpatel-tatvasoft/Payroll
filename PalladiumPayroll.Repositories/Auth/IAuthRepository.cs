using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumPayroll.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<JsonResult> Login(LoginRequest loginRequest);
    }
}
