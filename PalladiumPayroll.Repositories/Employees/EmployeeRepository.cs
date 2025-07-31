using Dapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dapper = new DapperContext(configuration);
            _httpContextAccessor = httpContextAccessor;
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
            parameters.Add("@SearchByName", string.IsNullOrEmpty(reqModel.Search) ? string.Empty : reqModel.Search);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employeeData = await _dapper.ExecuteStoredProcedure<EmployeeDataViewModel>("usp_GetEmployeeList", parameters);
            var totalCount = parameters.Get<int>("@TotalCount");
            return HttpStatusCodeResponse.SuccessResponse(new TableDataModel<EmployeeDataViewModel>
            {
                DataList = employeeData,
                TotalCount = totalCount
            }, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployee", parameters);
            return result;
        }

        public async Task<JsonResult> GetEmployeePaymentDetail(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var result = await _dapper.ExecuteStoredProcedureSingle<EmployeePaymentDetail>("usp_GetEmployeePaymentDetail", parameters);
            if (result == null)
            {
                // return HttpStatusCodeResponse.NotFoundResponse("Payment info");
            }
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "Payment info", ActionType.Retrieved));
        }

        public async Task<bool> EmployeePaymentDetailSave(EmployeePaymentDetail reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@PaymentTypeId", reqModel.PaymentMethod);
            parameters.Add("@AccountName", reqModel.AccountHolderName);
            parameters.Add("@AccountNumber", reqModel.AccountNumber);
            parameters.Add("@AccountTypeId", reqModel.TypeofAccount);
            parameters.Add("@BankId", reqModel.BankId);
            parameters.Add("@BranchCode", reqModel.BranchCode);
            parameters.Add("@AccountHolderRelation", reqModel.AccountHolderRelation);
            parameters.Add("@SplitPayment", reqModel.SplitPayment);
            parameters.Add("@AccountName1", reqModel.AccountHolderName1);
            parameters.Add("@AccountNumber1", reqModel.AccountNumber1);
            parameters.Add("@AccountTypeId1", reqModel.TypeofAccount1);
            parameters.Add("@BankId1", reqModel.BankId1);
            parameters.Add("@BranchCode1", reqModel.BranchCode1);
            parameters.Add("@AccountHolderRelation1", reqModel.AccountHolderRelation1);
            parameters.Add("@SplitAmount1", reqModel.SplitAmount1);
            parameters.Add("@SplitPercent1", reqModel.SplitPercent1);
            parameters.Add("@SplitAmount2", reqModel.SplitAmount2);
            parameters.Add("@SplitPercent2", reqModel.SplitPercent2);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeePaymentDetail", parameters);
            return result;
        }

        public async Task<JsonResult> GetCasualWageInformation(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var result = await _dapper.ExecuteStoredProcedureSingle<CasualWageInformation>("usp_GetCasualWageInformation", parameters);
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Casual Wage Information", ActionType.Retrieved));
        }

        public async Task<bool> UpdateCasualWageInformation(CasualWageInformation reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@NormalHour", reqModel.NormalHour);
            parameters.Add("@CasualOverTime", reqModel.CasualOverTime);
            parameters.Add("@HolidayRate", reqModel.HolidayRate);
            parameters.Add("@SundayRate", reqModel.SundayRate);
            parameters.Add("@NightHour", reqModel.NightHour);
            parameters.Add("@CasualNightOvertimeRate", reqModel.CasualNightOvertime);
            parameters.Add("@HolidayNightRate", reqModel.HolidayNightRate);
            parameters.Add("@SundayNightRate", reqModel.SundayNightRate);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertCasualWageInformation", parameters);
            return result;
        }
        public async Task<TransactionTypeDropdownsDTO> GetTransactionTypesDropdownData(long companyId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            return await _dapper.ExecuteStoredProcedureMultipleAsync(
                "usp_GetTransactionTypesDropdownData",
                parameters,
                async multi =>
                {
                    TransactionTypeDropdownsDTO? dropdownsData = new TransactionTypeDropdownsDTO
                    {
                        JobGrades = (await multi.ReadAsync<TransactionType>()).ToList(),
                    };
                    return dropdownsData;
                }
            );
        }

    }
}
