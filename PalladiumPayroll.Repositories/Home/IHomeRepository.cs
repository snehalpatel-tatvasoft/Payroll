using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<ApiResponse<object>> GetAllEmployeeList(int employeeId);
    }
}
