using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services;
using static PalladiumPayroll.Helper.Constants.AppConstants;

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
                return await _commonService.GetCountryList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [ResponseCache(Duration = 60)]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetTaxYearList()
        {
            try
            {
                return await _commonService.GetTaxYearList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBankList(int? companyId)
        {
            try
            {
                return await _commonService.GetBankList(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBranchList(int bankId)
        {
            try
            {
                return await _commonService.GetBranchList(bankId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [ResponseCache(Duration = 30)]
        [HttpGet("[action]")]
        public async Task<ActionResult> GetStandardIndustryCode()
        {
            try
            {
                return await _commonService.GetStandardIndustryCode();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetTradeClassification()
        {
            try
            {
                return await _commonService.GetTradeClassification();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> CheckDBConnection([FromQuery]DBConnectionModel dbConnectionModel)
        {
            try
            {
                return await _commonService.CheckDBConnection(dbConnectionModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTransactionList()
        {
            try
            {
                return await _commonService.GetTransactionList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message: "An error occurred on the server");
            }
        }
    }
}
