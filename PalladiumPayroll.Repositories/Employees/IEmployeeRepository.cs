using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;

namespace PalladiumPayroll.Repositories.Employees
{
    public interface IEmployeeRepository
    {
        Task<JsonResult> GetEmployeeFilters(int companyId);
        Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel);
        Task<bool> DeleteEmployee(int employeeId);
        Task<JsonResult> GetEmployeePaymentDetail(int employeeId);
        Task<bool> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel);
        Task<JsonResult> GetCasualWageInformation(int employeeId);
        Task<bool> CasualWageInformationSave(CasualWageInformation reqModel);
    }
}
