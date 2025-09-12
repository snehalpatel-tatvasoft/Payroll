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

        Task<int> UpdateBatchDetail(BatchInfoRequest reqModel);
        Task<bool> BatchLeaveImport(BatchLeaveImport reqModel, DataTable batchLeaveTable);
        Task<bool> UpsertBatchSingleLeave(BatchLeaveDetail reqModel);
        Task<List<BatchLeave>> GetExistingBatchList(long companyId);
        Task<List<BatchLeaveImportData>> GetImportBatchLeave(BatchInfoRequest reqModal);
        Task<bool> SaveImportBatchLeave(int batchId);
        Task<List<BatchLeaveImportActualData>> GetImportActualBatchLeave(int batchId);
    }
}
