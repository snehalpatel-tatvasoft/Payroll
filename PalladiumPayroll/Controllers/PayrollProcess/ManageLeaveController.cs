using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.PayrollProcess.ManageLeave;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Controllers.PayrollProcess
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageLeaveController : ControllerBase
    {
        private readonly IManageLeaveService _manageLeaveService;
        public ManageLeaveController(IManageLeaveService manageLeaveService)
        {
            _manageLeaveService = manageLeaveService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeLeaveDetail([FromQuery] EmployeeLeaveFilterViewModel reqModel)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeLeaveDetail(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeLeave(int leaveDetailId)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeLeave(leaveDetailId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel)
        {
            try
            {
                return await _manageLeaveService.UpsertEmployeeLeave(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetEmployeeBaseOnPeriodWithDueDays(int periodId)
        {
            try
            {
                return await _manageLeaveService.GetEmployeeBaseOnPeriodWithDueDays(periodId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            try
            {
                return await _manageLeaveService.UpdateBatchDetail(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpPost("[action]")]
        public async Task<ActionResult> BatchLeaveImport([FromForm] BatchLeaveImport reqModel)
        {
            try
            {
                return await _manageLeaveService.BatchLeaveImport(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> UpsertBatchSingleLeave(BatchLeaveDetail reqModel)
        {
            try
            {
                return await _manageLeaveService.UpsertBatchSingleLeave(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }


        [HttpGet("[action]")]
        public async Task<ActionResult> GetExistingBatchList(long companyId)
        {
            try
            {
                return await _manageLeaveService.GetExistingBatchList(companyId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetImportBatchLeave([FromQuery] BatchInfoRequest reqModal)
        {
            try
            {
                return await _manageLeaveService.GetImportBatchLeave(reqModal);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> SaveImportBatchLeave(int batchId)
        {
            try
            {
                return await _manageLeaveService.SaveImportBatchLeave(batchId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetImportActualBatchLeave(int batchId)
        {
            try
            {
                return await _manageLeaveService.GetImportActualBatchLeave(batchId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteExistingBatch(int batchId)
        {
            try
            {
                return await _manageLeaveService.DeleteExistingBatch(batchId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteLeaveBatch([FromQuery] int leaveDetailId, bool isActualLeave)
        {
            try
            {
                return await _manageLeaveService.DeleteLeaveBatch(leaveDetailId, isActualLeave);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetLeaveAttachment(int leaveDetailId, bool isActualLeave)
        {
            try
            {
                return await _manageLeaveService.GetLeaveAttachment(leaveDetailId, isActualLeave);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddLeaveAttachment([FromForm] AddBatchLeaveAttachment reqModel)
        {
            try
            {
                return await _manageLeaveService.AddLeaveAttachment(reqModel);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteLeaveAttachment([FromQuery] int documentLeaveId, string path, bool isActualLeave)
        {
            try
            {
                return await _manageLeaveService.DeleteLeaveAttachment(documentLeaveId, path, isActualLeave);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> DownloadLeaveAttachment(string fileUrl)
        {
            try
            {
                return File(await _manageLeaveService.DownloadLeaveAttachment(fileUrl), ContentTypes.OctetStream, fileUrl.Split("\\").LastOrDefault());
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetUnapprovedLeave(int cycleId)
        {
            try
            {
                return await _manageLeaveService.GetUnapprovedLeave(cycleId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> ApproveLeaves([FromBody] List<int> leaveDetailId)
        {
            try
            {
                return await _manageLeaveService.ApproveLeaves(leaveDetailId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetLeaveHistory(int leaveDetailId)
        {
            try
            {
                return await _manageLeaveService.GetLeaveHistory(leaveDetailId);
            }
            catch (Exception ex)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
            }
        }
    }
}
