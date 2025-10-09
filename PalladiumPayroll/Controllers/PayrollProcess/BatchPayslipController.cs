using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.BatchPayslip;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.BatchPayslip;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.PayrollProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchPayslipController : ControllerBase
    {
        private readonly IBatchPayslipService _batchPayslipService;
        public BatchPayslipController(IBatchPayslipService batchPayslipService)
        {
            _batchPayslipService = batchPayslipService;
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetExistingBatch(long companyId)
        {
            try
            {
                return await _batchPayslipService.GetExistingBatchList(companyId);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            try
            {
                return await _batchPayslipService.UpdateBatchDetail(reqModel);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> LoadPayslipTransaction(BatchPayslipInsert reqModel)
        {
            try
            {
                return await _batchPayslipService.LoadPayslipTransaction(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> MultiTransactionLoad([FromQuery] MultiTransactionGet reqModel)
        {
            try
            {
                return await _batchPayslipService.MultiTransactionLoad(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> BatchTransactionUpsertBulk(BatchPayslipBulkInsert reqModel)
        {
            try
            {
                return await _batchPayslipService.BatchTransactionUpsertBulk(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> BatchTransactionDeleteBulk(BatchPayslipBulkInsert reqModel)
        {
            try
            {
                return await _batchPayslipService.BatchTransactionDeleteBulk(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateTransactionDetail(BatchTransactionUpdate reqModel)
        {
            try
            {
                return await _batchPayslipService.UpdateTransactionDetail(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SaveBatchPayslip(BatchPayslipInsert reqModel)
        {
            try
            {
                return await _batchPayslipService.SaveBatchPayslip(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteBatchTransaction(long transactionId)
        {
            try
            {
                return await _batchPayslipService.DeleteBatchTransaction(transactionId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ProcessBatchPayslip(BatchPayslipProcess reqModel)
        {
            try
            {
                return await _batchPayslipService.ProcessBatchPayslip(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
