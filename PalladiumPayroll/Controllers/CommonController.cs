using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
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
        public async Task<ActionResult> CheckDBConnection([FromQuery] DBConnectionModel dbConnectionModel)
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

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBusinessTypeList()
        {
            try
            {
                return await _commonService.GetBusinessTypeList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetNumberOfEmployeesList()
        {
            try
            {
                return await _commonService.GetNumberOfEmployeesList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetIndustryOrSectorTypeList()
        {
            try
            {
                return await _commonService.GetIndustryOrSectorTypeList();
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddIndustrySectorType([FromBody] string newIndustrySector)
        {
            try
            {
                return await _commonService.AddIndustrySectorType(newIndustrySector);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteIndustrySector(int industrySectorId)
        {
            try
            {
                return await _commonService.DeleteIndustrySector(industrySectorId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
