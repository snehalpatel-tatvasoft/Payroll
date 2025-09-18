using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using System.Data;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public interface IManageLeaveRepository
    {
        Task<TableDataModel<EmployeeLeaveViewModel>> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel);
        Task<AddEmployeeLeaves?> GetEmployeeLeave(int leaveDetailId);
        Task<int> UpsertEmployeeLeave(AddEmployeeLeaves reqModel);

        Task<JsonResult> GetEmployeeBaseOnPeriodWithDueDays(int periodId);
        Task<int> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<bool> BatchLeaveImport(BatchLeaveImport reqModel, DataTable batchLeaveTable);
        Task<int> UpsertBatchSingleLeave(BatchLeaveDetail reqModel);
        Task<List<BatchLeave>> GetExistingBatchList(long companyId);
        Task<List<BatchLeaveImportData>> GetImportBatchLeave(BatchInfoRequest reqModal);
        Task<bool> SaveImportBatchLeave(int batchId);
        Task<List<BatchLeaveImportActualData>> GetImportActualBatchLeave(int batchId);
        Task<bool> DeleteExistingBatch(int batchId);
        Task<bool> DeleteLeaveBatch(int leaveDetailId, bool isActualLeave);

        Task<List<BatchLeaveAttachment>> GetLeaveAttachment(int leaveDetailId, bool isActualLeave);
        Task<bool> AddLeaveAttachment(BatchLeaveDocument data);
        Task<bool> DeleteLeaveAttachment(int documentLeaveId, bool isActualLeave);
        Task<List<UnApprovedLeaves>> GetUnapprovedLeave(int cycleId);
        Task<bool> ApproveLeaves(List<int> leaveDetailId);
        Task<List<LeaveHistory>> GetLeaveHistory(int leaveDetailId);
        Task<SPResultMessage> ProcessBatchLeave(int batchId);
        Task<bool> UnProcessBatchLeave(int batchId);
    }
}
