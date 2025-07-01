using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
        Task<PayrollSummaryResponse> GetPayrollSummaryData(int PayrollSetupId);
        Task<EmployeeTypeCountResponse> GetEmployeeTypeCount(int PayrollSetupId);
        Task<List<PayrollCycleDataResponse>> GetPayrollCycleData();
    }
}
