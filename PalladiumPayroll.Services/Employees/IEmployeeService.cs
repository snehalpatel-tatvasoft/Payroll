using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;

namespace PalladiumPayroll.Services.Employees
{
    public interface IEmployeeService
    {
        Task<JsonResult> GetEmployeeFilters(int companyId);
        Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel);
        Task<JsonResult> DeleteEmployee(int employeeId);
        Task<JsonResult> GetEmployeePaymentDetail(int employeeId);
        Task<JsonResult> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel);
        Task<JsonResult> GetCasualWageInformation(int employeeId);
        Task<JsonResult> CasualWageInformationSave(CasualWageInformation reqModel);
    }
}
