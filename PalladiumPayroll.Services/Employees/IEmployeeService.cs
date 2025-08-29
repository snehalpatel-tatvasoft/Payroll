using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Employees;

namespace PalladiumPayroll.Services.Employees
{
    public interface IEmployeeService
    {
        Task<JsonResult> GetEmployeeFilters(int companyId);
        Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel);
        Task<byte[]> ExportEmployeeList(EmployeeFilterViewModel reqModel);
        Task<JsonResult> DeleteEmployee(int employeeId);
        Task<JsonResult> GetEmployeePaymentDetail(int employeeId);
        Task<JsonResult> GetEmployeePersonalInfo(int employeeId);
        Task<JsonResult> GetEmployeePersonalInfoDropDown(int companyId);
        Task<JsonResult> SaveEmployeePersonalInfo(EmployeePersonalInformation reqModel);
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
        Task<JsonResult> GetPayrollTransactionList(TransactionReqModel reqModel);
        Task<JsonResult> SaveEmployeeTakeOnBalance(TransactionSaveModel reqModel);
        Task<JsonResult> GetEmployeeTakeOnBalance(int employeeId, int allowanceType);
        Task<JsonResult> DeleteEmployeeTakeOnBalance(List<int> takeOnBalanceIds);
        Task<JsonResult> SetTakeOnComplete(int employeeId);
        Task<JsonResult> DeleteEmployeeLoan(int employeeLoanId);
        Task<JsonResult> GetEmployeeLoanDetail(long employeeId);
        Task<JsonResult> GetGarnisheeDropdownData(long companyId);
        Task<JsonResult> GetGarnisheeDetails(long employeeId);
        Task<JsonResult> UpsertGarnishee(EmployeeGarnisheeRequest request);
        Task<JsonResult> GetSavingsDetails(long employeeId);
        Task<JsonResult> UpsertSaving(EmployeeSavingsRequest request);
        Task<TaxInformationDropdownData> GetTaxInformationDropdownData();
        Task<JsonResult> GetTaxInformation(int employeeId);
        Task<JsonResult> UpdateTaxInformation(TaxInformation reqModel);
        Task<JsonResult> GetEmployeeDocument(int employeeId);
        Task<JsonResult> UploadDocuments(EmployeeDocumentUpload employeeDocument);
        Task<JsonResult> DeleteDocuments(EmployeeDocumentDelete reqModel);
        Task<byte[]> DownloadDocument(string documentUrl);
        Task<JsonResult> GetEmployeeByEmployeeId(long employeeId, long companyId);
        Task<JsonResult> GetSecondApprovalEmployeeListByCompanyId(long companyId);
        Task<JsonResult> UpdateEmployeeSelfService(UpdateEmployeeSelfServiceModel model);
        Task<JsonResult> GetAccessRolesByCompanyId(long companyId);
        Task<JsonResult> GetPreviousService(int employeeId);
        Task<List<LeaveModel>> GetEmployeeLeaves(int employeeId);
        Task<JsonResult> SaveEmpLeaveEntitlementNew(EditLeaveRequest reqModel, string oprType);
        Task<JsonResult> DeleteEmployeeLeave(int employeeLeaveId);
    }
}
