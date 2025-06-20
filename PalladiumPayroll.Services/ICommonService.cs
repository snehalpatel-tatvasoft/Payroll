using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services
{
    public interface ICommonService
    {
        Task<List<CountryDropdownResponse>> GetCountryList();
    }
}
