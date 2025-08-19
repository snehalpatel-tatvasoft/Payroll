using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.PayslipDisplaySetup;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.CompanySettings;

public class PayslipDisplaySetupService : IPayslipDisplaySetupService
{
    private readonly IPayslipDisplaySetupRepository _payslipDisplaySetupRepository;

    public PayslipDisplaySetupService(IPayslipDisplaySetupRepository payslipDisplaySetupRepository)
    {
        _payslipDisplaySetupRepository = payslipDisplaySetupRepository;
    }

    public async Task<JsonResult> SavePayslipDisplaySettings(SavePayslipSettingsDTO request)
    {
        bool isSaved = await _payslipDisplaySetupRepository.SavePayslipDisplaySettings(request);

        if (!isSaved)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.PayslipDisplaySetupSaveFailed);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.PayslipDisplaySetup, ActionType.Saved));
    }

    public async Task<JsonResult> GetPayslipSettingsByCompanyId(int companyId)
    {
        PayslipDisplaySetupDataDTO? payslipDisplaySetupData = await _payslipDisplaySetupRepository.GetPayslipSettingsByCompanyId(companyId);

        if (payslipDisplaySetupData == null)
        {
            payslipDisplaySetupData = new PayslipDisplaySetupDataDTO
            {
                CompanyId = companyId,
                IsCompanyContribution = false,
                IsFringeBenefits = false,
                PayslipLayout = 2,
                PayslipMessage = string.Empty
            };
        }
        return HttpStatusCodeResponse.SuccessResponse(payslipDisplaySetupData, string.Empty);
    }
}
