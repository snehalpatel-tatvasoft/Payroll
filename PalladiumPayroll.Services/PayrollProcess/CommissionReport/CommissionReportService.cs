using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.Repositories.PayrollProcess.CommissionReport;

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

    public async Task<JsonResult> GetPayPeriods(int companyId, int cycleId)
    {
        return await _commissionReportRepository.GetPayPeriods(companyId, cycleId);
    }
}

