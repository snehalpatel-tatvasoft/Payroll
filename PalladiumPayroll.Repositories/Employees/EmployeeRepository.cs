using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.DTOs.Employees;
using PalladiumPayroll.DTOs.Miscellaneous;
using System.Data;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (result != null)
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
            parameters.Add("@ReportTo", reqModel.ReportTo);
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
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
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

        public async Task<JsonResult> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem)
        {
            var result = new List<DropDownViewModel>();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", reqItem.Name);
            parameters.Add("@CompanyId", reqItem.CompanyId);
            switch (reqItem.Type)
            {
                case 2:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddJobGrade", parameters);
                    break;
                case 3:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddOccupationLevel", parameters);
                    break;
                case 4:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddOccupationStatus", parameters);
                    break;
                case 5:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddOccupationCategory", parameters);
                    break;
                case 6:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddWSPCategory", parameters);
                    break;
                case 7:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddOFOCode", parameters);
                    break;
                case 8:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddMajorCostCenter", parameters);
                    break;
                case 9:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddRegion", parameters);
                    break;
                case 10:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddAppointmentType", parameters);
                    break;
                case 11:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddPayPoint", parameters);
                    break;
                case 12:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddNICGrade", parameters);
                    break;
                case 13:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddBranch", parameters);
                    break;
                case 14:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddDivision", parameters);
                    break;
                case 15:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddSubDivision", parameters);
                    break;
                case 16:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddMunicipality", parameters);
                    break;
                case 17:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddLocation", parameters);
                    break;
                case 18:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddDepartment", parameters);
                    break;
                case 19:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddProvince", parameters);
                    break;
                case 20:
                    result = await _dapper.ExecuteStoredProcedure<DropDownViewModel>("usp_AddOperationSupport", parameters);
                    break;
            }
            if (result.Count > 0 && result.FirstOrDefault()?.Id > 0)
            {
                return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Item", ActionType.Saved));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteWorkOrganizationalDropdownItem(int id, int type)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@type", type);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteOrgnizationDropDownItem", parameters);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, "Item", ActionType.Deleted));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var data = await _dapper.ExecuteStoredProcedureSingle<EmployeeOrgnizationalModel>("usp_GetEmployeeOrganization", parameters);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " organization", ActionType.Retrieving));
        }

        public async Task<JsonResult> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@StartDate", reqModel.StartDate);
            parameters.Add("@DesignationId", reqModel.DesignationId);
            parameters.Add("@JobGradeId", reqModel.JobGradeId);
            parameters.Add("@OccupationalLevelId", reqModel.OccupationalLevelId);
            parameters.Add("@OccupationalStatusId", reqModel.OccupationalStatusId);
            parameters.Add("@OccupationalCategoryId", reqModel.OccupationalCategoryId);
            parameters.Add("@WSPCategoryId", reqModel.WSPCategoryId);
            parameters.Add("@OFOCodeId", reqModel.OFOCodeId);
            parameters.Add("@MajorCostCenterId", reqModel.MajorCostCenterId);
            parameters.Add("@RegionId", reqModel.RegionId);
            parameters.Add("@AppointmentTypeId", reqModel.AppointmentTypeId);
            parameters.Add("@PayPointId", reqModel.PayPointId);
            parameters.Add("@NICGradeId", reqModel.NICGradeId);
            parameters.Add("@BranchId", reqModel.BranchId);
            parameters.Add("@DivisionId", reqModel.DivisionId);
            parameters.Add("@SubDivisionId", reqModel.SubDivisionId);
            parameters.Add("@MunicipalityId", reqModel.MunicipalityId);
            parameters.Add("@LocationId", reqModel.LocationId);
            parameters.Add("@DepartmentId", reqModel.DepartmentId);
            parameters.Add("@ProvinceId", reqModel.ProvinceId);
            parameters.Add("@SupportFunctionId", reqModel.SupportFunctionId);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeOrganization", parameters);
            if (result)
            {
                return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " organization", ActionType.Saving));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        #endregion

        #region TimeSheet SetUp
        public async Task<JsonResult> GetEmployeeTimeSheetSetup(long employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var data = await _dapper.ExecuteStoredProcedureSingle<TimeSheetSetup>("usp_GetEmployeeTimeSheetSetup", parameters);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Time Sheet", ActionType.Retrieved));
        }

        public async Task<JsonResult> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", timeSheetSetup.EmployeeId);
            parameters.Add("@EnableTimeSheet", timeSheetSetup.EnableTimeSheet);
            parameters.Add("@TimeSheetPassword", timeSheetSetup.TimeSheetConfirmPassword);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeTimeSheetSetup", parameters);
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " Time Sheet", ActionType.Saved));
        }
        #endregion

        #region Employee Self Service

        public async Task<JsonResult> GetEmployeeByEmployeeId(long employeeId, long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@CompanyId", companyId);

            var flatList = await _dapper.ExecuteStoredProcedure<EmployeeDetailForEmployeeSelfservice>(
                "usp_GetEmployeeByEmployeeIdForEmployeeSelfservice", parameters);

            if (flatList == null || !flatList.Any())
                return HttpStatusCodeResponse.NotFoundResponse("Employee not found.");

            var first = flatList.First();

            var response = new EmployeeSelfServiceResponse
            {
                Email = first.Email,
                Password = first.Password,
                AccessRoleID = first.AccessRoleID,
                AccessRoleName = first.AccessRoleName,
                IsManager = first.IsManager,
                SecondApprovalId = first.SecondApprovalId,
                SecondApprovalFullName = first.SecondApprovalFullName,
                Functionalities = flatList.Select(f => new FunctionalityPermissionDto
                {
                    FunctionalityId = f.FunctionalityId,
                    FunctionalityName = f.FunctionalityName,
                    View = f.View,
                    Edit = f.Edit,
                    Delete = f.Delete
                }).ToList()
            };

            return HttpStatusCodeResponse.SuccessResponse(response, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Retrieved));
        }

        public async Task<JsonResult> GetSecondApprovalEmployeeListByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var data = await _dapper.ExecuteStoredProcedure<SecondApprovalEmployeeDTO>("usp_GetSecondApprovalEmployeeListByCompanyId", parameters);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Second Approval Employees", ActionType.Retrieved));
        }


        public async Task<JsonResult> UpdateEmployeeSelfService(UpdateEmployeeSelfServiceModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", model.EmployeeId);
            parameters.Add("@CompanyId", model.CompanyId);
            parameters.Add("@IsManager", model.IsManager);
            parameters.Add("@SecondApprovalId", model.SecondApprovalId);

            // Create a DataTable for the TVP
            var functionalityTable = new DataTable();
            functionalityTable.Columns.Add("FunctionalityID", typeof(int));
            functionalityTable.Columns.Add("View", typeof(bool));
            functionalityTable.Columns.Add("Edit", typeof(bool));
            functionalityTable.Columns.Add("Delete", typeof(bool));

            // Populate the DataTable with the functionality list
            foreach (var functionality in model.FunctionalityList)
            {
                functionalityTable.Rows.Add(functionality.FunctionalityId, functionality.View, functionality.Edit, functionality.Delete);
            }

            parameters.Add("@FunctionalityList", functionalityTable.AsTableValuedParameter("UserFunctionalityListType"));

            var result = await _dapper.ExecuteStoredProcedureSingle<dynamic>("usp_UpdateEmployeeSelfService", parameters);
            if (result.Result == 1)
            {
                return HttpStatusCodeResponse.SuccessResponse(true, string.Format(ResponseMessages.Success, ResponseMessages.Employee, ActionType.Updated));
            }
            else
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error: {result.ErrorMessage} (Error Number: {result.ErrorNumber})");
            }
        }

        #endregion
    }
}
