using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories
{
    public interface ICommonRepository
    {
        Task<List<DropDownViewModel>> GetCountryList();
        Task<List<DropDownViewModel>> GetTaxYearList();
        Task<List<DropDownViewModel>> GetBankList(int? companyId);
        Task<List<DropDownViewModel>> GetBranchList(int bankId);
        Task<List<DropDownViewModel>> GetStandardIndustryCode();
        Task<List<DropDownViewModel>> GetTradeClassification();
        Task<List<TransactionList>> GetTransactionList();
        Task<bool> CheckDBConnection(DBConnectionModel dbConnectionModel);
    }
}
