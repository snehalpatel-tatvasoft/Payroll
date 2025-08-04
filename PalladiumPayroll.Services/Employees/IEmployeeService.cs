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
        Task<JsonResult> GetEmployeeWorkDropDown(int companyId);
        Task<JsonResult> GetEmployeeWorkInformation(int employeeId);
        Task<JsonResult> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel);
        Task<JsonResult> GetEmployeeWorkOrganizationalDropdownData(long companyId);
        Task<JsonResult> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem);
        Task<JsonResult> DeleteWorkOrganizationalDropdownItem(int id, int type);
        Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId);
        Task<JsonResult> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel);
        Task<JsonResult> GetEmployeeTimeSheetSetup(long employeeId);
        Task<JsonResult> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup);
        Task<JsonResult> GetCasualWageInformation(int employeeId);
        Task<JsonResult> UpdateCasualWageInformation(CasualWageInformation reqModel);
        Task<TransactionTypeDropdownsDTO> GetTransactionTypesDropdownData(long companyId);
        Task<JsonResult> AddDirective(DirectiveRequest reqModel);
        Task<List<GetDirectiveResponse>> GetDirectivesByEmployeeId(long employeeId);
        Task<JsonResult> UpdateDirective(long directiveId, DirectiveRequest reqModel);
        Task<JsonResult> DeleteDirective(long directiveId);
    }
}
