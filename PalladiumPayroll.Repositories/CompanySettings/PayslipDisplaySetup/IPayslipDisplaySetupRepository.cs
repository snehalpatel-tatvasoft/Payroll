using PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;

namespace PalladiumPayroll.Repositories.CompanySettings;

public interface IPayslipDisplaySetupRepository
{
    Task<bool> SavePayslipDisplaySettings(SavePayslipSettingsDTO request);

    Task<PayslipDisplaySetupDataDTO?> GetPayslipSettingsByCompanyId(int companyId);
}
