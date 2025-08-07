using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;

namespace PalladiumPayroll.Services.EmployeesLoan;

public interface IEmployeesLoanService
{
    Task<JsonResult> CreateEmployeeLoan(EmployeeLoanRequestDTO request);
    Task<JsonResult> UpdateEmployeeLoan(EmployeeLoanRequestDTO request);
    Task<JsonResult> PauseEmployeeLoan(long employeeLoanId, long updatedBy);
    Task<JsonResult> GetLoansByCompanyId(long companyId);
    Task<EmployeeLoanDropdownsDTO> GetEmployeeLoanDropdowns(long companyId);
    Task<JsonResult> FullPaidEmployeeLoan(long employeeLoanId, long updatedBy);
}
