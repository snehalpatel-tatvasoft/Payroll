using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;

namespace PalladiumPayroll.Services.CompanySettings;

public interface IPayslipDisplaySetupService
{
    Task<JsonResult> SavePayslipDisplaySettings(SavePayslipSettingsDTO request);

    Task<JsonResult> GetPayslipSettingsByCompanyId(int companyId);
}
