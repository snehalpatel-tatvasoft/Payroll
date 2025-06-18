using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories
{
    public interface ICommonRepository
    {
        Task<List<CountryDropdownResponse>> GetCountryList();
    }
}
