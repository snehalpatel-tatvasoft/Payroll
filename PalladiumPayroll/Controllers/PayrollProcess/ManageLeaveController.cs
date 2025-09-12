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
    }
}
