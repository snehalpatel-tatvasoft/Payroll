using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.BatchPayslip
{
    public interface IBatchPayslipService
    {
        Task<JsonResult> GetExistingBatchList(long companyId);
        Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<JsonResult> LoadPayslipTransaction(BatchPayslipInsert reqModel);
        Task<JsonResult> MultiTransactionLoad(MultiTransactionGet reqModel);
        Task<JsonResult> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel);
        Task<JsonResult> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel);
        Task<JsonResult> UpdateTransactionDetail(BatchTransactionUpdate reqModel);
        Task<JsonResult> SaveBatchPayslip(BatchPayslipInsert reqModel);
        Task<JsonResult> DeleteBatchTransaction(long transactionId);
        Task<JsonResult> ProcessBatchPayslip(BatchPayslipProcess reqModel);
    }
}
