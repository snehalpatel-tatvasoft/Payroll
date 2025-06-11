using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Home;

namespace PalladiumPayroll.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("GetAllEmployeeList")]
        public async Task<IActionResult> GetAllEmployeeList(int employeeId)
        {
            try
            {
                ApiResponse<object>? apiResponse = await _homeService.GetAllEmployeeList(employeeId);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return new JsonResult(new ApiResponse<string>(false, ex.Message, string.Empty));
            }
        }
    }
}
