using Microsoft.AspNetCore.Mvc;

namespace PalladiumPayroll.Services.Home
{
    public interface IHomeService
    {
        Task<JsonResult> GetAllEmployeeList(int employeeId);
    }
}
