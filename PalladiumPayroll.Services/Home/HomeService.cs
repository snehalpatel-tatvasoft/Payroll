using PalladiumPayroll.Repositories.Home;
using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Services.Home
{
    public class HomeService: IHomeService
    {
        public IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository) 
        {
            _homeRepository = homeRepository;
        }

        public async Task<ApiResponse<object>> GetAllEmployeeList(int employeeId)
        {
            return await _homeRepository.GetAllEmployeeList(employeeId);
        }
    }
}
