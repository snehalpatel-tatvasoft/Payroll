using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public interface IManageLeaveService
    {
        Task<JsonResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel);
        Task<JsonResult> GetEmployeeLeave(int leaveDetailId);
        Task<JsonResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel);

        Task<JsonResult> GetEmployeeBaseOnPeriodWithDueDays(int periodId);
        Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<JsonResult> BatchLeaveImport(BatchLeaveImport reqModel);
        Task<JsonResult> UpsertBatchSingleLeave(BatchLeaveDetail reqModel);
        Task<JsonResult> GetExistingBatchList(long companyId);
        Task<JsonResult> GetImportBatchLeave(BatchInfoRequest reqModal);
        Task<JsonResult> SaveImportBatchLeave(int batchId);
        Task<JsonResult> GetImportActualBatchLeave(int batchId);
        Task<JsonResult> DeleteExistingBatch(int batchId);
        Task<JsonResult> DeleteLeaveBatch(int leaveDetailId, bool isActualLeave);

        Task<JsonResult> GetLeaveAttachment(int leaveDetailId, bool isActualLeave);
        Task<JsonResult> AddLeaveAttachment(AddBatchLeaveAttachment reqModel);
        Task<JsonResult> DeleteLeaveAttachment(int documentLeaveId, string path, bool isActualLeave);
        Task<byte[]> DownloadLeaveAttachment(string documentUrl);

        Task<JsonResult> GetUnapprovedLeave(int cycleId);
        Task<JsonResult> ApproveLeaves(List<int> leaveDetailId);

        Task<JsonResult> GetLeaveHistory(int leaveDetailId);

        Task<JsonResult> ProcessBatchLeave(int batchId);
        Task<JsonResult> UnProcessBatchLeave(int batchId);
    }
}
