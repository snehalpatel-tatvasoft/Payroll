using Microsoft.AspNetCore.Mvc;

namespace PalladiumPayroll.Services.PayrollProcess.CommissionReport;

public interface ICommissionReportService
{
    Task<JsonResult> GetPayrollCycles(int companyId);
    Task<JsonResult> GetPayPeriods(int companyId, int cycleId);



}
