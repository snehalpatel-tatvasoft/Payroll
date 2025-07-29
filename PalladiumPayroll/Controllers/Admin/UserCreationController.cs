using Microsoft.AspNetCore.Mvc;
using System.Net;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Admin;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;

namespace PalladiumPayroll.Controllers.CompanySettings
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCreationController : ControllerBase
    {
        private readonly IUserCreationService _userCreationService;

        public UserCreationController(IUserCreationService userCreationService)
        {
            _userCreationService = userCreationService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] UserCreationRequestDTO request)
        {
            try
            {
                JsonResult? res = await _userCreationService.CreateUser(request);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] UserCreationRequestDTO request, Guid id)
        {
            try
            {
                JsonResult? res = await _userCreationService.UpdateUser(request, id);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("DeleteUser/{id}/{companyId}")]
        public async Task<ActionResult> DeleteUser(Guid id, long companyId)
        {
            try
            {
                JsonResult? res = await _userCreationService.DeleteUser(id, companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetUsersByCompanyId/{companyId}")]
        public async Task<ActionResult> GetUsersByCompanyId(long companyId)
        {
            try
            {
                JsonResult? res = await _userCreationService.GetUsersByCompanyId(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetAccessRolesNamesByCompanyId/{companyId}")]
        public async Task<ActionResult> GetAccessRolesNamesByCompanyId(long companyId)
        {
            try
            {
                JsonResult? res = await _userCreationService.GetAccessRolesNamesByCompanyId(companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpGet("GetUserById/{id}/{companyId}")]
        public async Task<ActionResult> GetUserById(Guid id, long companyId)
        {
            try
            {
                JsonResult? res = await _userCreationService.GetUserById(id, companyId);
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new HttpApiResponse<object>
                {
                    Result = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Data = null
                });
            }
        }
    }
}