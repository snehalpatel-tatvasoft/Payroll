using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Repositories.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _dapper;

        public EmployeeRepository(IConfiguration configuration)
        {
            _dapper = new DapperContext(configuration);
        }

        
        public async Task<JsonResult> GetEmployeeFilters(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var data = await _dapper.ExecuteStoredProcedureMultipleAsync("usp_GetFiltersForEmployee", parameters, async (multi) =>
            {
                var departmentList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                var designationList = (await multi.ReadAsync<DropDownViewModel>()).ToList();

               return new
               {
                   DepartmentList = departmentList,
                   DesignationList = designationList
               };
            });
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "Filter", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetEmployeeList(EmployeeFilterViewModel reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@DepartmentId", reqModel.DepartmentId);
            parameters.Add("@DesignationId", reqModel.DesignationId);
            parameters.Add("@Status", reqModel.IsActive);
            parameters.Add("@CurrentPage", reqModel.CurrentPage);
            parameters.Add("@PageSize", reqModel.PageSize);
            parameters.Add("@SortBy", reqModel.SortBy);
            parameters.Add("@SortType", reqModel.sortType == true ? SortAsc : SortDesc);
            parameters.Add("@SearchByName", reqModel.Search);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employeeData = await _dapper.ExecuteStoredProcedure<EmployeeDataViewModel>("usp_GetEmployeeList", parameters);
            var totalCount = parameters.Get<int>("@TotalCount");
            return HttpStatusCodeResponse.SuccessResponse(new TableDataModel<EmployeeDataViewModel>
            {
                DataList = employeeData,
                TotalCount = totalCount
            }, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }
    }
}
