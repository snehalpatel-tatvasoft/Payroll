using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company;

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
        Task<bool> AddIndustrySectorType(string industrySector);
        Task<List<DropDownViewModel>> GetBusinessTypeList();
        Task<List<DropDownViewModel>> GetNumberOfEmployeesList();
        Task<List<DropDownViewModel>> GetIndustryOrSectorTypeList();
        Task<bool> DeleteIndustrySector(int industrySectorId);
    }
}
