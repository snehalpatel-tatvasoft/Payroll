using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
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

        public async Task<List<CountryDropdownResponse>> GetCountryList()
        {
            return await _commonRepository.GetCountryList();
        }
    }
}
