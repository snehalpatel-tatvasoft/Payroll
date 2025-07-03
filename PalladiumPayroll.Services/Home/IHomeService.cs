using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
        Task<PayrollSummaryResponse> GetPayrollSummaryData(int PayrollSetupId,int CompanyId, string UserId);
        Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int PayrollSetupId, int CompanyId, string UserId);
        Task<List<PayrollCycleDataResponse>> GetPayrollCycleData(int CompanyId, string UserId);
    }
}
