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
        public async Task<JsonResult> GetAllEmployeeList(int employeeId)
        {
            try
            {
                return await _homeService.GetAllEmployeeList(employeeId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.BadRequestResponse();
            }
        }
    }
}
