using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public interface IManageLeaveRepository
    {
        Task<TableDataModel<EmployeeLeaveViewModel>> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel);
    }
}
