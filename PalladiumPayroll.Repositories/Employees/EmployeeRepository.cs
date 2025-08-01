using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;
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


        #region Payment info
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
        #endregion

        #region Work Info
        public async Task<JsonResult> GetEmployeeWorkDropDown(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            var result = await _dapper.ExecuteStoredProcedureMultipleAsync("usp_FetchEmployeeWorkInfoDropList", parameters, async (multi) =>
            {
                var cycleList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                var managerList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                var minimumWageList = (await multi.ReadAsync<DropDownViewModel>()).ToList();

                return new
                {
                    CycleList = cycleList,
                    ManagerList = managerList,
                    MinimumWageList = minimumWageList
                };
            });
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "work info drop list", ActionType.Retrieved));
        }

        public async Task<JsonResult> GetEmployeeWorkInformation(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var result = await _dapper.ExecuteStoredProcedureSingle<EmployeeWorkInformation>("usp_GetEmployeeWorkInfo", parameters);
            if(result != null)
            {
                var workDaySplit = result.StandardWorkingDays?
                            .Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                            .ToList();
                result.WorkingDay = workDaySplit;
            }
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "Work info", ActionType.Retrieved));

        }

        public async Task<bool> EmployeeWorkInfoSave(EmployeeWorkInformation reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@StartDate", reqModel.StartDate);
            parameters.Add("@DepartmentId", reqModel.DepartmentId);
            parameters.Add("@DesignationId", reqModel.DesignationId);
            parameters.Add("@PayrollCycle", reqModel.CycleType);
            //parameters.Add("@ReportTo", reqModel.ReportTo);
            parameters.Add("@IsCommission", reqModel.IsCommission);
            parameters.Add("@IsExcludeEFA", reqModel.IsExcludeEFA);
            parameters.Add("@RepCode", reqModel.RepCode);
            parameters.Add("@AnnualSalary", reqModel.AnnualSalary);
            parameters.Add("@MonthlySalary", reqModel.MonthlySalary);
            parameters.Add("@RatePerDay", reqModel.RatePerDay);
            parameters.Add("@RatePerHour", reqModel.RatePerHour);
            parameters.Add("@WorkingDay", string.Join(",", reqModel.WorkingDay));
            parameters.Add("@HoursPerMonth", reqModel.HoursPerMonth);
            parameters.Add("@HoursPerWeek", reqModel.HoursPerWeek);
            parameters.Add("@HoursPerDay", reqModel.HoursPerDay);
            parameters.Add("@DayPerMonth", reqModel.DayPerMonth);
            parameters.Add("@DayPerWeek", reqModel.DayPerWeek);
            parameters.Add("@MinimumWage", reqModel.MinimumWage);
            parameters.Add("@LeavePeriod", reqModel.LeavePeriod);
            parameters.Add("@LeaveYear", reqModel.LeaveYear);
            parameters.Add("@BonusPeriod", reqModel.BonusPeriod);
            parameters.Add("@BonusYear", reqModel.BonusYear);

            parameters.Add("@BCEAMonthlySalary", reqModel.BCEAMonthlySalary);
            parameters.Add("@BCEAVariableSalary", reqModel.BCEAVariableSalary);
            parameters.Add("@BCEAOverMonth", reqModel.BCEAOverMonth);
            parameters.Add("@BCEATotalRemuneration", reqModel.BCEATotalRemuneration);
            parameters.Add("@BCEATermination", reqModel.BCEATermination);
            parameters.Add("@BCEAPortionLeavePay", reqModel.BCEAPortionLeavePay);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeWorkInformation", parameters);
            return result;
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalDropdownData(long companyId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            return await _dapper.ExecuteStoredProcedureMultipleAsync(
                "usp_GetDropdownDataForWorkOrganization",
                parameters,
                async multi =>
                {
                    var dropdownsData = new
                    {
                        JobTitle = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        JobGrade = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        OccupationalLevels = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        OccupationalStatus = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        OccupationalCategories = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        WSPCategory = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        OFOCodes = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        MajorCostCenters = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Region = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Appointment = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        PayPoint = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        NICGrades = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Branches = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Division = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        SubDivision = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Municipality = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Location = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Department = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        Provinces = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                        OperationalSupport = (await multi.ReadAsync<DropDownViewModel>()).ToList(),
                    };
                    return HttpStatusCodeResponse.SuccessResponse(dropdownsData, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "work info drop list", ActionType.Retrieved));
                }
            );
        }
        #endregion

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
                        TransactionType = (await multi.ReadAsync<TransactionType>()).ToList(),
                    };
                    return dropdownsData;
                }
            );
        }
        public async Task<bool> AddDirective(DirectiveRequest reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@DirectiveNo", reqModel.DirectiveNumber);
            parameters.Add("@DirectiveDate", reqModel.DirectiveDate);
            parameters.Add("@SourceCode", reqModel.SourceCode);
            parameters.Add("@DirectiveAmount", reqModel.Amount);
            parameters.Add("@TypeIndicator", reqModel.TypeIndicator);
            parameters.Add("@IsActive", reqModel.IsActive);
            parameters.Add("@PayrollProcessId", reqModel.TransactionType);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_AddEmployeeDirective", parameters);
            return result;
        }
        public async Task<List<GetDirectiveResponse>> GetDirectivesByEmployeeId(long employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var result = await _dapper.ExecuteStoredProcedure<GetDirectiveResponse>(
                "usp_GetEmployeeDirectives",
                parameters
            );

            return result.ToList();
        }

        public async Task<bool> UpdateDirective(long directiveId, DirectiveRequest reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DirectiveId", directiveId);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@DirectiveNo", reqModel.DirectiveNumber);
            parameters.Add("@DirectiveDate", reqModel.DirectiveDate);
            parameters.Add("@SourceCode", reqModel.SourceCode);
            parameters.Add("@DirectiveAmount", reqModel.Amount);
            parameters.Add("@TypeIndicator", reqModel.TypeIndicator);
            parameters.Add("@IsActive", reqModel.IsActive);
            parameters.Add("@PayrollProcessId", reqModel.TransactionType);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateEmployeeDirective", parameters);
            return result;
        }
        public async Task<bool> DeleteDirective(long directiveId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DirectiveId", directiveId);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployeeDirective", parameters);
            return result;
        }


    }
}
