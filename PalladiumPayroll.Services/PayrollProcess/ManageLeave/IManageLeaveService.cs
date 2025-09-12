using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public interface IManageLeaveService
    {
        Task<JsonResult> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel);
        Task<JsonResult> GetEmployeeLeave(int leaveDetailId);
        Task<JsonResult> UpsertEmployeeLeave(AddEmployeeLeaves reqModel);

        Task<JsonResult> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<JsonResult> BatchLeaveImport(BatchLeaveImport reqModel);
        Task<JsonResult> UpsertBatchSingleLeave(BatchLeaveDetail reqModel);
        Task<JsonResult> GetExistingBatchList(long companyId);
        Task<JsonResult> GetImportBatchLeave(BatchInfoRequest reqModal);
        Task<JsonResult> SaveImportBatchLeave(int batchId);
        Task<JsonResult> GetImportActualBatchLeave(int batchId);
    }
}
