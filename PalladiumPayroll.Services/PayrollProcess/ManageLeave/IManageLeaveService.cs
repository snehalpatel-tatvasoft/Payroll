using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public interface IManageLeaveService
    {
        Task<JsonResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel);
    }
}
