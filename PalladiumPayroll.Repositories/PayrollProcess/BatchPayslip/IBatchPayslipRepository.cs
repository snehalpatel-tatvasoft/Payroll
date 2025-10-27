using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using System.Data;

namespace PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip
{
    public interface IBatchPayslipRepository
    {
        Task<List<BatchData>> GetExistingBatchList(long companyId);
        Task<BatchInfoWithBatchData> GetBatchInfo(int BatchId);
        Task<bool> DeleteExistingBatch(int batchId);
        Task<int> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<List<BatchPayslipTransaction>> LoadPayslipTransaction(int batchId, bool mode);
        Task<List<BatchPayslipLeave>> LoadPayslipEmployeeLeave(int batchId, bool mode);
        Task<int> BatchPayslipTransactionDetailInsert(BatchPayslipInsert reqModel);
        Task<MultiTransaction> GetMultiTransaction(MultiTransactionGet reqModel);
        Task<bool> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel);
        Task<bool> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel);
        Task<bool> ImportBatchTransactionUpsert(BatchPayslipInsert reqModel, DataTable importTransactionData, string fileName);
        Task<BatchPayslipTransaction?> UpdateTransactionDetail(BatchTransactionUpdate reqModel);
        Task<SPResultMessage> UpdateEmployeeLeaveDetail(PayslipLeave payslipLeave);
        Task<int> SaveBatchPayslip(BatchPayslipInsert reqModel);
        Task<bool> DeleteBatchTransaction(long transactionId);
        Task<SPResultMessage> ProcessBatchPayslip(BatchPayslipProcess reqModel);
    }
}
