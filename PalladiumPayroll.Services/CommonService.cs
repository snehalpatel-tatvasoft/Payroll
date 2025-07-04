using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories;

namespace PalladiumPayroll.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }

        public async Task<JsonResult> GetCountryList()
        {
            List<DropDownViewModel> countryList = await _commonRepository.GetCountryList();
            return HttpStatusCodeResponse.SuccessResponse(countryList, string.Empty);
        }

        public async Task<JsonResult> GetTaxYearList()
        {
            List<DropDownViewModel> yearList = await _commonRepository.GetTaxYearList();
            return HttpStatusCodeResponse.SuccessResponse(yearList, string.Empty);
        }

        public async Task<JsonResult> GetBankList(int? companyId)
        {
            List<DropDownViewModel> bankList = await _commonRepository.GetBankList(companyId);
            return HttpStatusCodeResponse.SuccessResponse(bankList, string.Empty);
        }

        public async Task<JsonResult> GetBranchList(int bankId)
        {
            List<DropDownViewModel> branchList = await _commonRepository.GetBranchList(bankId);
            return HttpStatusCodeResponse.SuccessResponse(branchList, string.Empty);
        }

        public async Task<JsonResult> GetStandardIndustryCode()
        {
            List<DropDownViewModel> standardList = await _commonRepository.GetStandardIndustryCode();
            return HttpStatusCodeResponse.SuccessResponse(standardList, string.Empty);
        }

        public async Task<JsonResult> GetTradeClassification()
        {
            List<DropDownViewModel> tradeList = await _commonRepository.GetTradeClassification();
            return HttpStatusCodeResponse.SuccessResponse(tradeList, string.Empty);
        }

        public async Task<JsonResult> CheckDBConnection(DBConnectionModel dbConnectionModel)
        {
            bool isConnected = await _commonRepository.CheckDBConnection(dbConnectionModel);
            if(isConnected)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, "Database connection is healthy.");
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse("Database connection failed");
        }
    }
}
