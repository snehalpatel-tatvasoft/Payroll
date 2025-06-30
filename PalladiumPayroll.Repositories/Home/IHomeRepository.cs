using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);

        Task<Dashboard> GetDashboardData(int CompanyId, string UserId);
    }
}
