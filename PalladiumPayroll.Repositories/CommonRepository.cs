using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
namespace PalladiumPayroll.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly DapperContext _dapper;
        public CommonRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<List<CountryDropdownResponse>> GetCountryList()
        {
            List<CountryDropdownResponse>? response = await _dapper.ExecuteStoredProcedure<CountryDropdownResponse>("sp_FetchCountry");
            return response;
        }
    }
}
