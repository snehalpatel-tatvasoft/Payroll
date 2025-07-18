using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings;

public interface IMinimumWageService
{
    Task<JsonResult> CreateMinimumWage(MinimumWageRequestDTO request);

    Task<JsonResult> GetMinimumWagesByCompanyId(int companyId);

    Task<JsonResult> UpdateMinimumWage(MinimumWageRequestDTO request);

    Task<JsonResult> DeleteMinimumWage(int wageId);
}
