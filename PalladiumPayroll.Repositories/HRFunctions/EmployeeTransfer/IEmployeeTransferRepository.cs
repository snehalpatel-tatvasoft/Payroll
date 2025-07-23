using PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;
namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;

public interface IEmployeeTransferRepository
{
    Task<EmployeeTransferDropdownsDTO> GetEmployeeTransferDropdownData(long companyId);
    Task<EmployeeTransferAutoFillDTO?> GetEmployeeAutoFillData(long employeeId, long companyId);
}
