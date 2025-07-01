using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
        Task<PayrollSummaryResponse> GetPayrollSummaryData(int CompanyId, int PayrollSetupId);
        Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int CompanyId, int PayrollSetupId);
        Task<List<PayrollCycleDataResponse>> GetPayrollCycleData(int CompanyId, string UserId);
    }
}
