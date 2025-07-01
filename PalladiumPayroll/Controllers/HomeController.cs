using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
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
        public async Task<JsonResult> GetAllEmployeeList([FromQuery]Employee employee)
        {
            try
            {
                var data = await _homeService.GetAllEmployeeList(2);
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.Employee, ex.Message));
            }
        }

        [HttpGet("GetPayrollSummaryData")]
        public async Task<JsonResult> GetPayrollSummaryData(int PayrollSetupId)
        {
            try
            {
                var data = await _homeService.GetPayrollSummaryData(PayrollSetupId);
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.PayrollSummary, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.PayrollSummary, ex.Message));
            }
        }

        [HttpGet("GetEmployeeTypeCount")]
        public async Task<JsonResult> GetEmployeeTypeCount(int PayrollSetupId)
        {
            try
            {
                var data = await _homeService.GetEmployeeTypeCount(PayrollSetupId);
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTypeCount, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTypeCount, ex.Message));
            }
        }
        [HttpGet("GetPayrollCycleData")]
        public async Task<JsonResult> GetPayrollCycleData()
        {
            try
            {
                var data = await _homeService.GetPayrollCycleData();
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeeTypeCount, ActionType.Retrieving));
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeeTypeCount, ex.Message));
            }
        }
    }
}
