using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.BatchPayslip
{
    public class BatchPayslipService : IBatchPayslipService
    {

        private readonly IBatchPayslipRepository _batchPayslipRepository;

        public BatchPayslipService(IBatchPayslipRepository batchPayslipRepository)
        {
            _batchPayslipRepository = batchPayslipRepository;
        }

        public async Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            var batchId = await _batchPayslipRepository.UpdateBatchDetail(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(batchId, string.Format(ResponseMessages.Success, "Batch pyaslip Detail", ActionType.Updated));
        }

        public async Task<JsonResult> LoadPayslipTransaction(BatchPayslipInsert reqModel)
        {
            var batchTransactionId = new BatchFirstTransaction();
            if(reqModel.BatchId == null || reqModel.BatchId == 0)
            {
                batchTransactionId = await _batchPayslipRepository.BatchPayslipTransactionDetailInsert(reqModel);
            }
            if(batchTransactionId != null)
            {
                var transaction = await _batchPayslipRepository.LoadPayslipTransaction(batchTransactionId.BatchId, reqModel.Mode?? false);
                var leaves = await _batchPayslipRepository.LoadPayslipEmployeeLeave(batchTransactionId.BatchId, reqModel.Mode ?? false);
                var data = new { transactionList = transaction , leaveList = leaves };
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch payslip Detail", "load"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse("No Record for this period");
        }

    }
}
