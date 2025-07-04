using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;

namespace PalladiumPayroll.Services
{
    public interface ICommonService
    {
        Task<JsonResult> GetCountryList();
        Task<JsonResult> GetTaxYearList();
        Task<JsonResult> GetBankList(int? companyId);
        Task<JsonResult> GetBranchList(int bankId);
        Task<JsonResult> GetStandardIndustryCode();
        Task<JsonResult> GetTradeClassification();
        Task<JsonResult> CheckDBConnection(DBConnectionModel dbConnectionModel);
    }
}
