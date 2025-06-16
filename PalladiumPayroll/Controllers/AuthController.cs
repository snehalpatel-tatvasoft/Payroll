using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs;
using System.Net;

namespace PalladiumPayroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController() { }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        //{
        //    try
        //    {
        //        CommonResponse<TokenDto> loginResponse = await authService.Login(loginRequest);

        //        return Ok(loginResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
