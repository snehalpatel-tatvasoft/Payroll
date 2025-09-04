using Microsoft.AspNetCore.Mvc;

namespace PalladiumPayroll.Repositories.PayrollProcess.CommissionReport;

public interface ICommissionReportRepository
{
    Task<JsonResult> GetPayrollCycles(int companyId);
    Task<JsonResult> GetPayPeriods(int companyId, int cycleId);

}
