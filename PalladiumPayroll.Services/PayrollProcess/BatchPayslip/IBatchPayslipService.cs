using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.BatchPayslip
{
    public interface IBatchPayslipService
    {
        Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<JsonResult> LoadPayslipTransaction(BatchPayslipInsert reqModel);
    }
}
