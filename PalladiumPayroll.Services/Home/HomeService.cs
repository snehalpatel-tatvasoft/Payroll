using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs;
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

        public async Task<List<Employee>> GetAllEmployeeList(int employeeId)
        {
            return await _homeRepository.GetAllEmployeeList(employeeId);
        }
    }
}
