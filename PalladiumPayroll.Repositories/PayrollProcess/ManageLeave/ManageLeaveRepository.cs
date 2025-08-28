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
            parameters.Add("@StratDate", reqModel.SatrtDate);
            parameters.Add("@EndDate", reqModel.EndDate);
            parameters.Add("@LeaveType", reqModel.LeaveTyepId);
            parameters.Add("@CurrentPage", reqModel.CurrentPage);
            parameters.Add("@PageSize", reqModel.PageSize);
            parameters.Add("@SortBy", reqModel.SortBy);
            parameters.Add("@SortType", reqModel.sortType == true ? SortAsc : SortDesc);
            parameters.Add("@SearchByName", string.IsNullOrEmpty(reqModel.Search) ? string.Empty : reqModel.Search);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employeeData = await _dapper.ExecuteStoredProcedure<EmployeeLeaveViewModel>("usp_GetEmployeeLeaveList", parameters);
            var totalCount = parameters.Get<int>("@TotalCount");
            return new TableDataModel<EmployeeLeaveViewModel>
            {
                DataList = employeeData,
                TotalCount = totalCount
            };
        }
    }
}
