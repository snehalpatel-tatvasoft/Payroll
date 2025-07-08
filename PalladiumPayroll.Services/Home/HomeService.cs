using Microsoft.AspNetCore.Http;
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

        public async Task<PayrollSummaryResponse> GetPayrollSummaryData(int PayrollSetupId, int CompanyId, string UserId)
        {
            return await _homeRepository.GetPayrollSummaryData(CompanyId, PayrollSetupId, UserId);
        }

        public async Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int PayrollSetupId, int CompanyId, string UserId)
        {
            return await _homeRepository.GetEmployeeTypeCount(CompanyId, PayrollSetupId, UserId);
        }

        public async Task<List<PayrollCycleDataResponse>> GetPayrollCycleData(int CompanyId, string UserId)
        {
            List<PayrollCycleDataResponse> res = new();

            var data = await _homeRepository.GetPayrollCycleData(CompanyId, UserId);

            if(data != null)
            {
                return data;
            }
            return res;
        }
    }
}
