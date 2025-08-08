using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;

namespace PalladiumPayroll.Services.CompanySettings
{
    public interface ITimesheetSetupService
    {
        Task<JsonResult> GetPayrollCycles(long companyId);
        Task<JsonResult> GetTimesheetPayrollSetup(long companyId);
        Task<JsonResult> UpsertTimesheetPayrollSetup(TimesheetSetupRequestDTO request);
    }
}