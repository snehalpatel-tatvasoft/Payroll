using Microsoft.AspNetCore.Mvc;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public interface ISinglePayslipService
{
    Task<JsonResult> GetPayrollCycleDropdown(long companyId);

    Task<JsonResult> GetNextUnprocessedPeriod(long companyId, long companyPayrollId);
}
