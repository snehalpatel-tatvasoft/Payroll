using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Helper.ImportExport;
using PalladiumPayroll.Repositories.PayrollProcess.ManageLeave;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public class ManageLeaveService : IManageLeaveService
    {
        private readonly IManageLeaveRepository _manageLeaveRepository;
        private readonly DirectoryPathSetting _directoryPathSetting;

        public ManageLeaveService(IManageLeaveRepository manageLeaveRepository, AppSettingPathHelper directoryPathSetting)
        {
            _manageLeaveRepository = manageLeaveRepository;
            _directoryPathSetting = directoryPathSetting.GetAppSettingDirectoryPath();
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


        public async Task<JsonResult> GetEmployeeBaseOnPeriodWithDueDays(int periodId)
        {
            return await _manageLeaveRepository.GetEmployeeBaseOnPeriodWithDueDays(periodId);
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
            var leaveDetailId = await _manageLeaveRepository.UpsertBatchSingleLeave(reqModel);
            if (leaveDetailId > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse<int?[]>([reqModel.BatchId, leaveDetailId], string.Format(ResponseMessages.Success, "Batch Leave", ActionType.Saved));
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

        public async Task<JsonResult> DeleteExistingBatch(int batchId)
        {
            var res = await _manageLeaveRepository.DeleteExistingBatch(batchId);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch Leave", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch Leave", ActionType.Deleting));
        }

        public async Task<JsonResult> DeleteLeaveBatch(int leaveDetailId, bool isActualLeave)
        {
            var res = await _manageLeaveRepository.DeleteLeaveBatch(leaveDetailId, isActualLeave);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Leave", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Leave", ActionType.Deleting));
        }


        public async Task<JsonResult> GetLeaveAttachment(int leaveDetailId, bool isActualLeave)
        {
            var data = await _manageLeaveRepository.GetLeaveAttachment(leaveDetailId, isActualLeave);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "leave attchament", ActionType.Retrieved));
        }

        public async Task<JsonResult> AddLeaveAttachment(AddBatchLeaveAttachment reqModel)
        {
            var basePath = _directoryPathSetting.LeaveAttachment;
            var leaveFolder = $"Leave_{reqModel.LeaveDetailId}";
            var finalPath = FileHandler.CombinePath(basePath, leaveFolder);
            FileHandler.CreateDirectory(finalPath);
            var filePath = Path.Combine(finalPath, reqModel.File.FileName);
            var isFileUploded =  await FileHandler.UploadFile(filePath, reqModel.File);
            if (isFileUploded)
            {
                var relativePath = Path.Combine(leaveFolder, reqModel.File.FileName).Replace(Path.DirectorySeparatorChar.ToString(), "/");

                var res = await _manageLeaveRepository.AddLeaveAttachment(new BatchLeaveDocument()
                {
                    LeaveDetailId = reqModel.LeaveDetailId,
                    EmployeeId = reqModel.EmployeeId,
                    BatchId = reqModel.BatchId,
                    IsActualLeave = reqModel.IsActualLeave,
                    DocFileUrl = relativePath,
                    DocFileName = reqModel.File.FileName,
                });

                if (res)
                {
                    return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Leave attachment", ActionType.uploaded));
                }
                return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Leave attachment", ActionType.uploaded));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Leave attachment", ActionType.uploaded));
        }

        public async Task<JsonResult> DeleteLeaveAttachment(int documentLeaveId, string path, bool isActualLeave)
        {
            var res = await _manageLeaveRepository.DeleteLeaveAttachment(documentLeaveId, isActualLeave);
            if (res)
            {
                var basePath = _directoryPathSetting.LeaveAttachment;
                FileHandler.DeleteFile(FileHandler.CombinePath(basePath, path));
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Leave attachment", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Leave attachment", ActionType.Deleting));
        }

        public async Task<byte[]> DownloadLeaveAttachment(string documentUrl)
        {
            var basePath = _directoryPathSetting.LeaveAttachment;
            return await FileHandler.ReadFileBytes(FileHandler.CombinePath(basePath, documentUrl));
        }


        public async Task<JsonResult> GetUnapprovedLeave(int cycleId)
        {
            var data = await _manageLeaveRepository.GetUnapprovedLeave(cycleId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Unapproved leave", ActionType.Retrieved));
        }

        public async Task<JsonResult> ApproveLeaves(List<int> leaveDetailId)
        {
            var res = await _manageLeaveRepository.ApproveLeaves(leaveDetailId);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Leave", "Approved"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Leave", "Approving"));
        }

        public async Task<JsonResult> GetLeaveHistory(int leaveDetailId)
        {
            var data = await _manageLeaveRepository.GetLeaveHistory(leaveDetailId);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Leave history", ActionType.Retrieved));
        }

        public async Task<JsonResult> ProcessBatchLeave(int batchId)
        {
            var resultData = await _manageLeaveRepository.ProcessBatchLeave(batchId);
            if (resultData.Result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, resultData.Message);
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(resultData.Message);
        }

        public async Task<JsonResult> UnProcessBatchLeave(int batchId)
        {
            var res = await _manageLeaveRepository.UnProcessBatchLeave(batchId);
            if (res)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Batch leave", "Unprocessed"));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Failed, "Batch leave", "Unprocessing"));
        }
    }
}
