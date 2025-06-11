using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<ApiResponse<object>> GetAllEmployeeList(int employeeId);
    }
}
