using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;

namespace PalladiumPayroll.Services.PayrollProcess.CommissionReport;

public interface ICommissionReportService
{
    Task<JsonResult> GetPayrollCycles(int companyId);
    Task<JsonResult> GetPayPeriods(int cycleId);
    Task<JsonResult> ImportCommissions(ImportCommissionRequestDTO request);
    Task<JsonResult> GetCommissions(int companyId, int? cycleId);
    Task<JsonResult> ProcessCommission(ProcessCommissionRequestDTO request);

}
