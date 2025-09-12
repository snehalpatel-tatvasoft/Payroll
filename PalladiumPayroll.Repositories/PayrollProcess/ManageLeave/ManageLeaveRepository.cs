using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using PalladiumPayroll.DTOs.Miscellaneous.Constants;
using System.Data;
using System.Numerics;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.PayrollProcess.ManageLeave
{
    public class ManageLeaveRepository : IManageLeaveRepository
    {
        private readonly DapperContext _dapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManageLeaveRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DapperContext(configuration);
            _httpContextAccessor = httpContextAccessor;
        }

        #region Leave Detail
        public async Task<TableDataModel<EmployeeLeaveViewModel>> GetEmployeeLeaveDetail(EmployeeLeaveFilterViewModel reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@StartDate", reqModel.StartDate);
            parameters.Add("@EndDate", reqModel.EndDate);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@LeaveType", reqModel.LeaveType);
            parameters.Add("@LeaveStatus", reqModel.LeaveStatus);
            parameters.Add("@CurrentPage", reqModel.CurrentPage);
            parameters.Add("@PageSize", reqModel.PageSize);
            parameters.Add("@SortBy", string.IsNullOrEmpty(reqModel.SortBy) ? "LastUpdatedDate" : reqModel.SortBy);
            parameters.Add("@SortType", reqModel.SortType == true ? SortAsc : SortDesc);
            parameters.Add("@Search", string.IsNullOrEmpty(reqModel.Search) ? string.Empty : reqModel.Search);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employeeData = await _dapper.ExecuteStoredProcedure<EmployeeLeaveViewModel>("usp_GetEmployeeLeaveList", parameters);
            var totalCount = employeeData.FirstOrDefault()?.TotalCount;
            return new TableDataModel<EmployeeLeaveViewModel>
            {
                DataList = employeeData,
                TotalCount = totalCount ?? 0
            };
        }

        public async Task<AddEmployeeLeaves?> GetEmployeeLeave(int leaveDetailId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeaveDetailId", leaveDetailId);
            return await _dapper.ExecuteStoredProcedureSingle<AddEmployeeLeaves>("usp_GetEmployeeLeave", parameters);
        }

        public async Task<int> UpsertEmployeeLeave(AddEmployeeLeaves reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LeaveDetailId", reqModel.LeaveDetailId);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@LeaveTypeId", reqModel.LeaveType);
            parameters.Add("@LeaveStatusId", reqModel.LeaveStatus);
            parameters.Add("@StartDate", reqModel.FromDate);
            parameters.Add("@StartType", reqModel.FromTime);
            parameters.Add("@EndDate", reqModel.ToDate);
            parameters.Add("@EndType", reqModel.ToTime);
            parameters.Add("@Duration", reqModel.Duration);
            parameters.Add("@RequestedDate", reqModel.RequestedDate);
            parameters.Add("@Comment", reqModel.Comment);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<int>("usp_UpsertEmployeeLeave", parameters);
        }
        #endregion

        #region Batch Leave
        public async Task<int> UpdateBatchDetail(BatchInfoRequest reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@Description", reqModel.BatchDescription);
            parameters.Add("@BatchName", reqModel.BatchNumber);
            parameters.Add("@CycleId", reqModel.PayrollCycle);
            parameters.Add("@PeriodId", reqModel.ProcessPeriod);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<int>("usp_UpsertBatchLeaveInfo", parameters);
        }

        public async Task<bool> BatchLeaveImport(BatchLeaveImport reqModel, DataTable batchLeaveTable)
        {
            if(reqModel.BatchId <= 0)
            {
                reqModel.BatchId = await UpdateBatchDetail(reqModel);
            }
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@IsActualLeave", reqModel.IsActualLeave);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            parameters.Add("@LeaveRecord", batchLeaveTable.AsTableValuedParameter("LeaveBatchImportType"));
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_ImportBatchLeave", parameters);
        }

        public async Task<bool> UpsertBatchSingleLeave(BatchLeaveDetail reqModel)
        {
            if (reqModel.BatchId <= 0)
            {
                reqModel.BatchId = await UpdateBatchDetail(reqModel);
            }
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", reqModel.BatchId);
            parameters.Add("@IsActualLeave", reqModel.IsActualLeave);
            parameters.Add("@LeaveDetailId", reqModel.LeaveDetailId);
            parameters.Add("@EmployeeCode", reqModel.EmployeeCode);
            parameters.Add("@LeaveType", reqModel.LeaveType);
            parameters.Add("@DateFrom", reqModel.DateFrom);
            parameters.Add("@DateTo", reqModel.DateTo);
            parameters.Add("@DueDays", reqModel.DueDays);
            parameters.Add("@Comment", reqModel.Comment);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst(JWTClaimTypes.UserId)?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpserttBatchSingleLeave", parameters);
        }

        public async Task<List<BatchLeave>> GetExistingBatchList(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            return await _dapper.ExecuteStoredProcedure<BatchLeave>("usp_GetExistingBatch", parameters);
        }

        public async Task<List<BatchLeaveImportData>> GetImportBatchLeave(BatchInfoRequest reqModal)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchName", reqModal.BatchNumber);
            parameters.Add("@CompanyPayrollId", reqModal.PayrollCycle);
            parameters.Add("@ProcessCycleId", reqModal.ProcessPeriod);
            return await _dapper.ExecuteStoredProcedure<BatchLeaveImportData>("usp_GetImportBatchLeave", parameters);
        }

        public async Task<bool> SaveImportBatchLeave(int batchId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", batchId);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_SaveImportBatchLeave", parameters);
        }

        public async Task<List<BatchLeaveImportActualData>> GetImportActualBatchLeave(int batchId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BatchId", batchId);
            return await _dapper.ExecuteStoredProcedure<BatchLeaveImportActualData>("usp_GetImportActualBatchLeave", parameters);
        }

        #endregion
    }
}
