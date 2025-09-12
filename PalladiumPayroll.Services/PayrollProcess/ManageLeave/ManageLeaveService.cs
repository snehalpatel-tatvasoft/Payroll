using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper.ImportExport;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public class ManageLeaveService : IManageLeaveService
    {
        private readonly IManageLeaveRepository _manageLeaveRepository;
        public ManageLeaveService(IManageLeaveRepository manageLeaveRepository)
        {
            _manageLeaveRepository = manageLeaveRepository;
        }

        public async Task<JsonResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel)
        {
            var data =  await _manageLeaveRepository.GetEmployeeLeaveDetail(reqModel);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetEmployeeLeave(int leaveDetailId)
        {
            var data = await _manageLeaveRepository.GetEmployeeLeave(leaveDetailId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel)
        {
            var res = await _manageLeaveRepository.UpsertEmployeeLeave(reqModel);
            if (res == 1)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Updated));
            }
            else if(res == 2)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.AlreadyExist, "Leave date is"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }


        public async Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            var batchId = await _manageLeaveRepository.UpdateBatchDetail(reqModel);
            if (batchId > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch Detail", ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Detail", ActionType.Updated));
        }

        public async Task<JsonResult> BatchLeaveImport(BatchLeaveImport reqModel)
        {
            var headerColumn = new DataColumn[]
            {
                new DataColumn("EmployeeCode", typeof(string)), new DataColumn("LeaveType", typeof(int)),
                new DataColumn("DateFrom", typeof(DateTime)), new DataColumn("DateTo", typeof(DateTime)),
                new DataColumn("DueDays", typeof(decimal)), new DataColumn("Comment", typeof(string))
            };
            var excelData = ExcelHelper.ImportFromExcel(reqModel.File, true, headerColumn);
            var res = await _manageLeaveRepository.BatchLeaveImport(reqModel, excelData);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(reqModel.BatchId, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Leave", ActionType.Imported));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> UpsertBatchSingleLeave(BatchLeaveDetail reqModel)
        {
            var res = await _manageLeaveRepository.UpsertBatchSingleLeave(reqModel);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(reqModel.BatchId, string.Format(ResponseMessages.Success, "Batch Leave", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Leave", ActionType.Saved));
        }

        public async Task<JsonResult> GetExistingBatchList(long companyId)
        {
            var data = await _manageLeaveRepository.GetExistingBatchList(companyId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch list", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetImportBatchLeave(BatchInfoRequest reqModal)
        {
            var data = await _manageLeaveRepository.GetImportBatchLeave(reqModal);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch leave", ActionType.Retrieved));
        }

        public async Task<JsonResult> SaveImportBatchLeave(int batchId)
        {
            var res = await _manageLeaveRepository.SaveImportBatchLeave(batchId);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch Leave", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Leave", ActionType.Saved));
        }

        public async Task<JsonResult> GetImportActualBatchLeave(int batchId)
        {
            var data = await _manageLeaveRepository.GetImportActualBatchLeave(batchId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Batch leave", ActionType.Retrieved));
        }

    }
}
