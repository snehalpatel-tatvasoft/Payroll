using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.PayrollProcess.CommissionReport;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;


namespace PalladiumPayroll.Services.PayrollProcess.CommissionReport;

public class CommissionReportService : ICommissionReportService
{
    private readonly ICommissionReportRepository _commissionReportRepository;

    public CommissionReportService(ICommissionReportRepository commissionReportRepository)
    {
        _commissionReportRepository = commissionReportRepository;
    }
    public async Task<JsonResult> GetPayrollCycles(int companyId)
    {
        return await _commissionReportRepository.GetPayrollCycles(companyId);
    }

    public async Task<JsonResult> GetPayPeriods(int cycleId)
    {
        return await _commissionReportRepository.GetPayPeriods(cycleId);
    }
    public async Task<JsonResult> ImportCommissions(ImportCommissionRequestDTO request)
    {
        var errorMessage = await _commissionReportRepository.ImportCommissions(request);
        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport, ActionType.Imported));
    }
    public async Task<JsonResult> GetCommissions(int companyId, int? cycleId)
    {
        return await _commissionReportRepository.GetCommissions(companyId, cycleId);
    }

    public async Task<JsonResult> ProcessCommission(ProcessCommissionRequestDTO request)
    {
        var errorMessage = await _commissionReportRepository.ProcessCommission(request);
        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.CommissionReport, ActionType.Saved));
    }
}

