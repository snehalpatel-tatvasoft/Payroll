using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.PayrollProcess.MangeLeave;
using System.Data;
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
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<int>("usp_UpsertEmployeeLeave", parameters);
        }
    }
}
