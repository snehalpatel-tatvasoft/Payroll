using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeCodes;

namespace PalladiumPayroll.Services.CompanySettings;

public interface IEmployeeCodesService
{
    Task<JsonResult> SaveEmployeeCodeGeneration(EmployeeCodeRequestDTO request);

    Task<JsonResult> GetEmployeeCodeByCompanyId(int companyId);
}
