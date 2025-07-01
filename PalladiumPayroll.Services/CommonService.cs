using PalladiumPayroll.DTOs.DTOs.Common;
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

        public async Task<List<DropDownViewModel>> GetCountryList()
        {
            return await _commonRepository.GetCountryList();
        }

        public async Task<List<DropDownViewModel>> GetBankList(int? companyId)
        {
            return await _commonRepository.GetBankList(companyId);
        }

        public async Task<List<DropDownViewModel>> GetBranchList(int bankId)
        {
            return await _commonRepository.GetBranchList(bankId);
        }

        public async Task<List<DropDownViewModel>> GetStandardIndustryCode()
        {
            return await _commonRepository.GetStandardIndustryCode();
        }

        public async Task<List<DropDownViewModel>> GetTradeClassification()
        {
            return await _commonRepository.GetTradeClassification();
        }
    }
}
