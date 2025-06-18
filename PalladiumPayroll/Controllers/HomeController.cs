using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Home;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("GetAllEmployeeList")]
        public async Task<JsonResult> GetAllEmployeeList(int employeeId)
        {
            try
            {
                var data = await _homeService.GetAllEmployeeList(employeeId);
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }

        [HttpGet("GetDashboardData")]
        public async Task<JsonResult> GetDashboardData()
        {
            try
            {
                var data = await _homeService.GetDashboardData();
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Dashboard, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Dashboard, ex.Message));
            }
        }
    }
}
