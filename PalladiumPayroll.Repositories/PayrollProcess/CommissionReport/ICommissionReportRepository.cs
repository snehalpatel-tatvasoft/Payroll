using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.PayrollProcess;

namespace PalladiumPayroll.Repositories.PayrollProcess.CommissionReport;

public interface ICommissionReportRepository
{
    Task<JsonResult> GetPayrollCycles(int companyId);
    Task<JsonResult> GetPayPeriods(int cycleId);
    Task<string?> ImportCommissions(ImportCommissionRequestDTO request);
    Task<JsonResult> GetCommissions(int companyId, int? cycleId);
    Task<string?> ProcessCommission(ProcessCommissionRequestDTO request);



}
