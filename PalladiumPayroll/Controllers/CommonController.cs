using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services;

namespace PalladiumPayroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("GetCountryList")]
        public async Task<IActionResult> GetCountryList()
        {
            try
            {
                List<CountryDropdownResponse> countryList = await _commonService.GetCountryList();
                return HttpStatusCodeResponse.SuccessResponse(countryList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }
    }
}
