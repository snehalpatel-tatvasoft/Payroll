using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.ManageLeave;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.PayrollProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageLeaveController : ControllerBase
    {
        private readonly IManageLeaveService _manageLeaveService;
        public ManageLeaveController(IManageLeaveService manageLeaveService)
        {
            _manageLeaveService = manageLeaveService;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeLeaveDetail(reqModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
