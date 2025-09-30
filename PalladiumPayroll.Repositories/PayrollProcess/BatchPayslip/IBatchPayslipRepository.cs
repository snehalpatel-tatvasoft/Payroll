using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip
{
    public interface IBatchPayslipRepository
    {
        Task<int> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<List<BatchPayslipTransaction>> LoadPayslipTransaction(int batchId, bool mode);
        Task<List<BatchPayslipLeave>> LoadPayslipEmployeeLeave(int batchId, bool mode);
        Task<BatchFirstTransaction?> BatchPayslipTransactionDetailInsert(BatchPayslipInsert reqModel);
    }
}
