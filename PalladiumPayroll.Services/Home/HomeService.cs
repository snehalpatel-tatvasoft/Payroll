using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using PalladiumPayroll.Repositories.Home;

namespace PalladiumPayroll.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<List<Employee>> GetAllEmployeeList(int employeeId)
        {
            return await _homeRepository.GetAllEmployeeList(employeeId);
        }

        public async Task<Dashboard> GetDashboardData()
        {
            int CompanyId = 1;
            string UserId = string.Empty;
            return await _homeRepository.GetDashboardData(CompanyId, UserId);
        }
    }
}
