using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip
{
    public interface IBatchPayslipRepository
    {
        Task<int> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<List<BatchPayslipTransaction>> LoadPayslipTransaction(int batchId, bool mode);
        Task<List<BatchPayslipLeave>> LoadPayslipEmployeeLeave(int batchId, bool mode);
        Task<int> BatchPayslipTransactionDetailInsert(BatchPayslipInsert reqModel);
        Task<List<SpecialTransaction>> GetSpecialRunTransaction(MultiTransactionGet reqModel);
        Task<List<BatchPayslipTransaction>> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel);
        Task<bool> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel);
        Task<BatchPayslipTransaction?> UpdateTransactionDetail(BatchTransactionUpdate reqModel);
        Task<int> SaveBatchPayslip(BatchPayslipInsert reqModel);
    }
}
