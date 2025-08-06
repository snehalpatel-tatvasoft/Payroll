using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeTransfer;
using PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;
namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeTransfer;

public interface IEmployeeTransferRepository
{
    Task<EmployeeTransferDropdownsDTO> GetEmployeeTransferDropdownData(long companyId);
    Task<EmployeeTransferAutoFillDTO?> GetEmployeeAutoFillData(long employeeId, long companyId);
    Task<EmployeeTransferDetailDTO> AddEmployeeTransfer(EmployeeTransferRequestDTO request);
    Task<List<EmployeeTransferDisplayDataModel>> GetEmployeeTransferList(long companyId);


}
