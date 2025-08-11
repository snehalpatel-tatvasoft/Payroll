using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
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
        Task<List<DropDownViewModel>> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem);
        Task<bool> DeleteWorkOrganizationalDropdownItem(int id, int type);
        Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId);
        Task<bool> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel);

        Task<JsonResult> GetEmployeeTimeSheetSetup(long employeeId);
        Task<bool> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup);

        Task<JsonResult> GetCasualWageInformation(int employeeId);
        Task<bool> UpdateCasualWageInformation(CasualWageInformation reqModel);
        Task<TransactionTypeDropdownsDTO> GetTransactionTypesDropdownData(long companyId);
        Task<bool> AddDirective(DirectiveRequest reqModel);
        Task<List<GetDirectiveResponse>> GetDirectivesByEmployeeId(long employeeId);
        Task<bool> UpdateDirective(long directiveId, DirectiveRequest reqModel);
        Task<bool> DeleteDirective(long directiveId);
        Task<List<PayrollTransactionList>> GetPayrollTransactionList(TransactionReqModel reqModel);
        Task<bool> SaveEmployeeTakeOnBalance(TransactionSaveModel reqModel);
        Task<TakeOnBalanceListWithTakeOnComplete> GetEmployeeTakeOnBalance(int employeeId, int allowanceType);
        Task<bool> DeleteEmployeeTakeOnBalance(List<int> takeOnBalanceIds);
        Task<bool> SetTakeOnComplete(int employeeId);
        Task<EmployeeLoanResponse> GetEmployeeLoanDetail(long employeeId);
        Task<bool> DeleteEmployeeLoan(int employeeLoanId);
        Task<GarnisheeDropdownListDto> GetGarnisheeDropdownData(long companyId);
        Task<bool> UpsertGarnishee(EmployeeGarnisheeRequest request);
        Task<List<GarnishDetails>> GetGarnisheeDetails(long employeeId);
        Task<bool> UpsertSaving(EmployeeSavingsRequest request);
        Task<List<SavingsDetails>> GetSavingsDetails(long employeeId);
        Task<TaxInformationDropdownData> GetTaxInformationDropdownData();
        Task<JsonResult> GetTaxInformation(int employeeId);
        Task<bool> UpdateTaxInformation(TaxInformation reqModel);

        Task<List<EmployeeDocuments>> GetEmployeeDocument(int employeeId);
        Task<bool> UploadDocumentsSave(List<EmployeeDocuments> employeeDocuments, int employeeId);
        Task<bool> DeleteDocuments(int documentId);

        Task<List<EmployeePreviousService>> GetPreviousService(int employeeId);
    }

}
