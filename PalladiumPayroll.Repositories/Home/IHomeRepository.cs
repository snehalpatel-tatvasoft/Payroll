using PalladiumPayroll.DTOs.DTOs;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<List<Employee>> GetAllEmployeeList(int employeeId);
    }
}
