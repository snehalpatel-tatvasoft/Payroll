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

        Task<JsonResult> GetEmployeeWorkDropDown(int companyId);
        Task<JsonResult> GetEmployeeWorkInformation(int employeeId);
        Task<bool> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel);
        Task<JsonResult> GetEmployeeWorkOrganizationalDropdownData(long companyId);
        Task<JsonResult> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem);
        Task<JsonResult> DeleteWorkOrganizationalDropdownItem(int id, int type);
        Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId);
        Task<JsonResult> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel);
        Task<JsonResult> GetEmployeeTimeSheetSetup(long employeeId);
        Task<JsonResult> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup);
        Task<JsonResult> GetEmployeeByEmployeeId(long employeeId, long companyId);
        Task<JsonResult> GetSecondApprovalEmployeeListByCompanyId(long companyId);
        Task<JsonResult> UpdateEmployeeSelfService(UpdateEmployeeSelfServiceModel model);
    }
}
