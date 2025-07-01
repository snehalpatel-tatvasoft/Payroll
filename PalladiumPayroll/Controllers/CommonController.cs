using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
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

        [ResponseCache(Duration = 30)]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetCountryList()
        {
            try
            {
                List<DropDownViewModel> countryList = await _commonService.GetCountryList();
                return HttpStatusCodeResponse.SuccessResponse(countryList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBankList(int? companyId)
        {
            try
            {
                List<DropDownViewModel> bankList = await _commonService.GetBankList(companyId);
                return HttpStatusCodeResponse.SuccessResponse(bankList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBranchList(int bankId)
        {
            try
            {
                List<DropDownViewModel> bankList = await _commonService.GetBranchList(bankId);
                return HttpStatusCodeResponse.SuccessResponse(bankList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }

        [ResponseCache(Duration = 30)]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetStandardIndustryCode()
        {
            try
            {
                List<DropDownViewModel> bankList = await _commonService.GetStandardIndustryCode();
                return HttpStatusCodeResponse.SuccessResponse(bankList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetTradeClassification()
        {
            try
            {
                List<DropDownViewModel> bankList = await _commonService.GetTradeClassification();
                return HttpStatusCodeResponse.SuccessResponse(bankList, string.Empty);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }
    }
}
