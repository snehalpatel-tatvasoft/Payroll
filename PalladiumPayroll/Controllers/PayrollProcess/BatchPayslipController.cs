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
        public async Task<ActionResult> GetExistingBatch(int companyId)
        {
            try
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
                //return await _batchPayslipService.GetExistingBatch(companyId);
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

    }
}
