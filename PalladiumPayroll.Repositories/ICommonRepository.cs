using PalladiumPayroll.DTOs.DTOs.Common;

namespace PalladiumPayroll.Repositories
{
    public interface ICommonRepository
    {
        Task<List<DropDownViewModel>> GetCountryList();
        Task<List<DropDownViewModel>> GetBankList(int? companyId);
        Task<List<DropDownViewModel>> GetBranchList(int bankId);
        Task<List<DropDownViewModel>> GetStandardIndustryCode();
        Task<List<DropDownViewModel>> GetTradeClassification();
    }
}
