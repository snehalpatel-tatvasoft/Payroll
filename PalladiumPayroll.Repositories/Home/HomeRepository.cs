using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Home
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DapperContext _dapper;

        public HomeRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        public async Task<List<Employee>> GetAllEmployeeList(int employeeId)
        {
            List<Employee> employee = new List<Employee>();

            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", employeeId);
            var data = await _dapper.ExecuteStoredProcedure<Employee>("usp_FetchEmployeeList", parameters);

            if (data.Count != 0)
            {
                employee = data;
            }
            return employee;
        }

        public async Task<EmployeeList> GetAllEmployeeDataList(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", employeeId);

            return await _dapper.ExecuteStoredProcedureMultipleAsync("usp_FetchEmployeeList", parameters, async (multi) =>
            {
                var employeeList = (await multi.ReadAsync<Employee>()).ToList();
                var total = (await multi.ReadAsync<int>()).FirstOrDefault();

                return new EmployeeList
                {
                    Employees = employeeList,
                    EmployeeCount = total
                };
            });
        }

        public async Task<PayrollSummaryResponse> GetPayrollSummaryData(int CompanyId, int PayrollSetupId, string UserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", CompanyId);
            parameters.Add("@PayrollSetupId", PayrollSetupId);
            parameters.Add("@UserId", UserId);

            return await _dapper.ExecuteStoredProcedureSingle<PayrollSummaryResponse>("usp_GetPayrollSummaryData", parameters);
        }

        public async Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int CompanyId, int PayrollSetupId, string UserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", CompanyId);
            parameters.Add("@PayrollSetupId", PayrollSetupId);
            parameters.Add("@UserId", UserId);

            return await _dapper.ExecuteStoredProcedureSingle<EmployeeTypeCountResponse>("usp_GetEmployeeTypeCount", parameters);
        }

        public async Task<List<PayrollCycleDataResponse>> GetPayrollCycleData(int CompanyId, string UserId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", CompanyId);
            parameters.Add("@UserId", UserId);

            return await _dapper.ExecuteStoredProcedure<PayrollCycleDataResponse>("usp_GetPayrollCycleData", parameters);
        }
    }
}
