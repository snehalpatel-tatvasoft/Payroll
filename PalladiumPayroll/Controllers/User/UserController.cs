using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Admin;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.User;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ChangeEmail(ChangeEmailModel reqModel)
        {
            try
            {
                return await _userService.ChangeEmail(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel reqModel)
        {
            try
            {
                return await _userService.ChangePassword(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.TryLater);
            }
        }
    }
}
