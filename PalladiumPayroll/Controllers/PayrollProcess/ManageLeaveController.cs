using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> GetEmployeeLeaveDetail([FromQuery]EmployeeLeaveFilterViewModel reqModel)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeLeaveDetail(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeLeave(int leaveDetailId)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeLeave(leaveDetailId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel)
        {
            try
            {
                return await _manageLeaveService.UpsertEmployeeLeave(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
