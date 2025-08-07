using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.EmployeesLoan;

namespace PalladiumPayroll.Repositories.EmployeesLoan;

public interface IEmployeesLoanRepository
{
    Task<bool> CreateEmployeeLoan(EmployeeLoanRequestDTO request);
    Task<bool> UpdateEmployeeLoan(EmployeeLoanRequestDTO request);
    Task<bool> PauseEmployeeLoan(long employeeLoanId, long updatedBy);
    Task<TableDataModel<EmployeeLoanResponseDTO>> GetLoansByCompanyId(LoanFilterViewModel reqModel);    Task<EmployeeLoanDropdownsDTO> GetEmployeeLoanDropdowns(long companyId);
    Task<bool> FullPaidEmployeeLoan(long employeeLoanId, long updatedBy);


}
