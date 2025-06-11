using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.Miscellaneous;

namespace PalladiumPayroll.Repositories.Home
{
    public interface IHomeRepository
    {
        Task<JsonResult> GetAllEmployeeList(int employeeId);
    }
}
