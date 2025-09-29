using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetExistingBatch(int reqModel)
        {
            try
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
                //return await _batchPayslipService.CheckCompanyExist(reqModel);
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

        [HttpGet("[action]")]
        public async Task<ActionResult> LoadPayslipTransaction(int batchId, bool mode)
        {
            try
            {
                return await _batchPayslipService.LoadPayslipTransaction(batchId, mode);
            }
            catch (Exception)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
