using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Repositories.Home;

namespace PalladiumPayroll.Services.Home
{
    public class HomeService : IHomeService
    {
        public IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public async Task<JsonResult> GetAllEmployeeList(int employeeId)
        {
            return await _homeRepository.GetAllEmployeeList(employeeId);
        }
    }
}
