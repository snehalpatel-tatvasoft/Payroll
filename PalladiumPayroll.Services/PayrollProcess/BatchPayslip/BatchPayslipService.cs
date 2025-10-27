using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper.ImportExport;
using PalladiumPayroll.Repositories.PayrollProcess.BatchPayslip;
using System.Data;
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

        public async Task<JsonResult> GetExistingBatchList(long companyId)
        {
            List<BatchData> batchList = await _batchPayslipRepository.GetExistingBatchList(companyId);
            return HttpStatusCodeResponse.SuccessResponse(batchList, string.Format(ResponseMessages.Success, "Batch List", "load"));
        }

        public async Task<JsonResult> GetBatchInfo(int batchId)
        {
            BatchInfoWithBatchData batchInfo = await _batchPayslipRepository.GetBatchInfo(batchId);
            if (batchInfo != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(batchInfo, string.Format(ResponseMessages.Success, "Batch Info", "load"));
            }
            return HttpStatusCodeResponse.NotFoundResponse("Batch Info");
        }

        public async Task<JsonResult> DeleteExistingBatch(int batchId)
        {
            bool isDeleted = await _batchPayslipRepository.DeleteExistingBatch(batchId);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(isDeleted, string.Format(ResponseMessages.Success, "Batch", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch", ActionType.Deleting));
        }

        public async Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            int batchId = await _batchPayslipRepository.UpdateBatchDetail(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(batchId, string.Format(ResponseMessages.Success, "Batch pyaslip Detail", ActionType.Updated));
        }

        public async Task<JsonResult> LoadPayslipTransaction(BatchPayslipInsert reqModel)
        {
            int batchId = 0;
            if(reqModel.BatchId == null || reqModel.BatchId == 0)
            {
                batchId = await _batchPayslipRepository.BatchPayslipTransactionDetailInsert(reqModel);
            }
            else
            {
                batchId = reqModel.BatchId ?? 0;
            }
            if(batchId > 0)
            {
                var transaction = await _batchPayslipRepository.LoadPayslipTransaction(batchId, reqModel.Mode ?? false);
                var leaves = await _batchPayslipRepository.LoadPayslipEmployeeLeave(batchId, reqModel.Mode ?? false);
                var data = new { transactionList = transaction , leaveList = leaves };
                return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch payslip Detail", "load"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch payslip Detail", "load"));
        }

        public async Task<JsonResult> MultiTransactionLoad(MultiTransactionGet reqModel)
        {
            MultiTransaction data = await _batchPayslipRepository.GetMultiTransaction(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "multi Transaction", "load"));
        }

        public async Task<JsonResult> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel)
        {
            bool isUpserted = await _batchPayslipRepository.BatchTransactionUpsertBulk(reqModel);
            if (isUpserted)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Updating));
        }

        public async Task<JsonResult> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel)
        {
            bool isDeleted = await _batchPayslipRepository.BatchTransactionDeleteBulk(reqModel);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(isDeleted, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Deleted));
        }

        public async Task<JsonResult> ImportBatchTransactionUpsert(ImportBatchPayslipBulkInsert reqModel)
        {
            var headerColumn = new DataColumn[]
            {
                new DataColumn("EmployeeCode", typeof(string)), new DataColumn("TransactionType", typeof(string)),
                new DataColumn("TransactionName", typeof(string)), new DataColumn("Unit", typeof(decimal)),
                new DataColumn("Value", typeof(decimal)), new DataColumn("IsRecurring", typeof(bool))
            };
            DataTable excelData = ExcelHelper.ImportFromExcel(reqModel.BatchTransaction, true, headerColumn);
            if (excelData.Rows.Count > 0)
            {
                bool isImported = await _batchPayslipRepository.ImportBatchTransactionUpsert(reqModel, excelData, reqModel.BatchTransaction.FileName);
                if (isImported)
                {
                    return HttpStatusCodeResponse.SuccessResponse(isImported, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Imported));
                }
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Importing));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.NotFound, "Transaction"));
        }

        public async Task<JsonResult> UpdateTransactionDetail(BatchTransactionUpdate reqModel)
        {
            BatchPayslipTransaction? transaction = await _batchPayslipRepository.UpdateTransactionDetail(reqModel);
            if (transaction != null)
            {
                return HttpStatusCodeResponse.SuccessResponse(transaction, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Updated));
        }

        public async Task<JsonResult> UpdateEmployeeLeaveDetail(PayslipLeave payslipLeave)
        {
            SPResultMessage res = await _batchPayslipRepository.UpdateEmployeeLeaveDetail(payslipLeave);
            if (res.Result)
            {
                return HttpStatusCodeResponse.SuccessResponse(res, string.Format(ResponseMessages.Success, "Employee Leave", ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(res.Message ?? string.Format(ResponseMessages.Failed, "Employee Leave", ActionType.Updated));
        }

        public async Task<JsonResult> SaveBatchPayslip(BatchPayslipInsert reqModel)
        {
            int batchId = await _batchPayslipRepository.SaveBatchPayslip(reqModel);
            if (batchId > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse(batchId, string.Format(ResponseMessages.Success, "Batch Payslip", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Payslip", ActionType.Saved));
        }

        public async Task<JsonResult> DeleteBatchTransaction(long transactionId)
        {
            bool isDeleted = await _batchPayslipRepository.DeleteBatchTransaction(transactionId);
            if (isDeleted)
            {
                return HttpStatusCodeResponse.SuccessResponse(isDeleted, string.Format(ResponseMessages.Success, "Batch Transaction", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Deleted));
        }

        public async Task<JsonResult> ProcessBatchPayslip(BatchPayslipProcess reqModel)
        {
            SPResultMessage res = await _batchPayslipRepository.ProcessBatchPayslip(reqModel);
            if (res.Result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch Payslip", ActionType.Processed));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Transaction", ActionType.Processing));
        }
    }
}
