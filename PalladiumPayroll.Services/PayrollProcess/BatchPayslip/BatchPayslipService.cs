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
            int? batchId = 0;
            if(reqModel.BatchId == null || reqModel.BatchId == 0)
            {
                batchId = await _batchPayslipRepository.BatchPayslipTransactionDetailInsert(reqModel);
            }
            if(batchId != null && batchId > 0)
            {
                var transaction = await _batchPayslipRepository.LoadPayslipTransaction(batchId ?? 0, reqModel.Mode?? false);
                var leaves = await _batchPayslipRepository.LoadPayslipEmployeeLeave(batchId ?? 0, reqModel.Mode ?? false);
                var data = new { transactionList = transaction , leaveList = leaves };
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch payslip Detail", "load"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }


        public async Task<JsonResult> MultiTransactionLoad(MultiTransactionGet reqModel)
        {
            var transaction = await _batchPayslipRepository.GetSpecialRunTransaction(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(transaction, string.Format(ResponseMessages.Success, "multi Transaction", "load"));
        }

        public async Task<JsonResult> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel)
        {
            var transaction = await _batchPayslipRepository.BatchTransactionUpsertBulk(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(transaction, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Updated));
        }

        public async Task<JsonResult> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel)
        {
            var isDeleted = await _batchPayslipRepository.BatchTransactionDeleteBulk(reqModel);
            if (isDeleted)
                return HttpStatusCodeResponse.SuccessResponse(isDeleted, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Deleted));
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> UpdateTransactionDetail(BatchTransactionUpdate reqModel)
        {
            var transaction = await _batchPayslipRepository.UpdateTransactionDetail(reqModel);
            if (transaction != null)
                return HttpStatusCodeResponse.SuccessResponse(transaction, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Updated));
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

    }
}
