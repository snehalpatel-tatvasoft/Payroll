using Microsoft.AspNetCore.Mvc;
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

        public async Task<JsonResult> LoadPayslipTransaction(int batchId, bool mode)
        {
            var transaction = await _batchPayslipRepository.LoadPayslipTransaction(batchId, mode);
            var leaves = await _batchPayslipRepository.LoadPayslipEmployeeLeave(batchId, mode);
            var data = new { transactionList = transaction , leaveList = leaves };
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch payslip Detail", "load"));
        }

    }
}
