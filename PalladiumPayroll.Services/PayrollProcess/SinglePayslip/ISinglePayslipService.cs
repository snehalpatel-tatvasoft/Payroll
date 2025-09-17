using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.SinglePayslip;

namespace PalladiumPayroll.Services.PayrollProcess.SinglePayslip;

public interface ISinglePayslipService
{
    Task<JsonResult> GetEmployeesForProcessing(GetEmployeesForProcessingRequestDTO request);
}
