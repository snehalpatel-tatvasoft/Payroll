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

        public async Task<PayrollSummaryResponse> GetPayrollSummaryData(int PayrollSetupId)
        {
            int CompanyId = 1;
            return await _homeRepository.GetPayrollSummaryData(CompanyId, PayrollSetupId);
        }

        public async Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int PayrollSetupId)
        {
            int CompanyId = 1;
            return await _homeRepository.GetEmployeeTypeCount(CompanyId, PayrollSetupId);
        }

        public async Task<List<PayrollCycleDataResponse>> GetPayrollCycleData()
        {
            List<PayrollCycleDataResponse> res = new();
            int CompanyId = 20;
            string UserId = "d1ae5f39-ad09-4cf8-b7da-c3a9bb8b9ab2";

            var data = await _homeRepository.GetPayrollCycleData(CompanyId, UserId);

            if(data != null)
            {
                return data;
            }
            return res;
        }
    }
}
