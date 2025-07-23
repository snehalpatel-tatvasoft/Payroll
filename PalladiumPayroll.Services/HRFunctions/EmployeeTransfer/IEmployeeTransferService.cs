using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.HRFunctions.EmployeeTransfer;

namespace PalladiumPayroll.Services.HRFunctions.EmployeeTransfer;

public interface IEmployeeTransferService
{
    Task<EmployeeTransferDropdownsDTO> GetEmployeeTransferDropdownData(long companyId);
    Task<JsonResult> GetEmployeeAutoFillData(long employeeId, long companyId);
}
