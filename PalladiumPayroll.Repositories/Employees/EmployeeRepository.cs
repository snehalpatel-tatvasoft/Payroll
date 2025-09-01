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

        public async Task<TableDataModel<EmployeeDataViewModel>> GetEmployeeList(EmployeeFilterViewModel reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@DepartmentId", reqModel.DepartmentId);
            parameters.Add("@DesignationId", reqModel.DesignationId);
            parameters.Add("@Status", reqModel.IsActive);
            parameters.Add("@CurrentPage", reqModel.CurrentPage);
            parameters.Add("@PageSize", reqModel.PageSize);
            parameters.Add("@SortBy", reqModel.SortBy);
            parameters.Add("@SortType", reqModel.SortType == true ? SortAsc : SortDesc);
            parameters.Add("@SearchByName", string.IsNullOrEmpty(reqModel.Search) ? string.Empty : reqModel.Search);
            parameters.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var employeeData = await _dapper.ExecuteStoredProcedure<EmployeeDataViewModel>("usp_GetEmployeeList", parameters);
            var totalCount = parameters.Get<int>("@TotalCount");
            return new TableDataModel<EmployeeDataViewModel>
            {
                DataList = employeeData,
                TotalCount = totalCount
            };
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployee", parameters);
            return result;
        }

        #region Personal Information

        public async Task<JsonResult> GetEmployeePersonalInfo(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var result = await _dapper.ExecuteStoredProcedureSingle<EmployeePersonalInformation>("usp_GetEmployeePersonalInfo", parameters);
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "Personal info", ActionType.Retrieved));
        }
        public async Task<JsonResult> GetEmployeePersonalInfoDropDown(int companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);
            var result = await _dapper.ExecuteStoredProcedureMultipleAsync("usp_GetEmployeePersonalInfoDropDown", parameters, async (multi) =>
            {
                var profileList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                var countryList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
                var cityList = (await multi.ReadAsync<DropDownViewModel>()).ToList();

                return new
                {
                    ProfileList = profileList,
                    CountryList = countryList,
                    CityList = cityList
                };
            });
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Employee + "Personal info drop list", ActionType.Retrieved));
        }
        public async Task<bool> SaveEmployeePersonalInfo(EmployeePersonalInformation reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@EmployeeCode", reqModel.EmployeeCode);
            parameters.Add("@EmployeeName", reqModel.EmployeeName);
            parameters.Add("@EmployeeSurname", reqModel.EmployeeSurname);
            parameters.Add("@Title", reqModel.Title);
            parameters.Add("@Initials", reqModel.Initials);
            parameters.Add("@ProfilePicture", reqModel.ProfilePicture);
            parameters.Add("@Gender", reqModel.Gender);
            parameters.Add("@PreferredName", reqModel.PreferredName);
            parameters.Add("@PassportIssuedBy", reqModel.PassportIssuedBy);
            parameters.Add("@DateOfBirth", reqModel.DateOfBirth);
            parameters.Add("@HomeNumber", reqModel.HomeNumber);
            parameters.Add("@CellNumber", reqModel.CellNumber);
            parameters.Add("@Email", reqModel.Email);
            parameters.Add("@EmergencyContactName", reqModel.EmergencyContactName);
            parameters.Add("@EmergencyRelation", reqModel.EmergencyRelation);
            parameters.Add("@EmergencyCellNumber", reqModel.EmergencyCellNumber);
            parameters.Add("@HomeLanguage", reqModel.HomeLanguage);
            parameters.Add("@Profile", reqModel.Profile);
            parameters.Add("@IDNumber", reqModel.IDNumber);
            parameters.Add("@PassportNumber", reqModel.PassportNumber);
            parameters.Add("@Race", reqModel.Race);
            parameters.Add("@EmploymentStatus", reqModel.EmploymentStatus);
            parameters.Add("@NatureOfPerson", reqModel.NatureOfPerson);
            parameters.Add("@IsPersonwithDisability", reqModel.IsPersonwithDisability);
            parameters.Add("@IsForeignNational", reqModel.IsForeignNational);
            parameters.Add("@IsRefugee", reqModel.IsRefugee);
            parameters.Add("@IsAsylumSeeker", reqModel.IsAsylumSeeker);
            parameters.Add("@AsylumPermitNumber", reqModel.AsylumPermitNumber);
            parameters.Add("@AddressIndicator", reqModel.AddressIndicator);
            parameters.Add("@UnitNumber", reqModel.UnitNumber);
            parameters.Add("@ComplexName", reqModel.ComplexName);
            parameters.Add("@StreetNumber", reqModel.StreetNumber);
            parameters.Add("@StreetName", reqModel.StreetName);
            parameters.Add("@District", reqModel.District);
            parameters.Add("@City", reqModel.City);
            parameters.Add("@Phy_PostalCode", reqModel.Phy_PostalCode);
            parameters.Add("@Phy_CountryId", reqModel.Phy_CountryId);
            parameters.Add("@IsPostalSame", reqModel.IsPostalSame);
            parameters.Add("@Address1", reqModel.Address1);
            parameters.Add("@Address2", reqModel.Address2);
            parameters.Add("@Address3", reqModel.Address3);
            parameters.Add("@Pos_PostalCode", reqModel.Pos_PostalCode);
            parameters.Add("@Pos_CountryId", reqModel.Pos_CountryId);
            parameters.Add("@UserId", reqModel.UserId);
            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeePersonalInfo", parameters);
            return result;
        }
        #endregion

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

        public async Task<List<DropDownViewModel>> AddWorkOrganizationalDropdownItem(WorkOrgnizationItem reqItem)
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
            return result;
        }

        public async Task<bool> DeleteWorkOrganizationalDropdownItem(int id, int type)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            parameters.Add("@type", type);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteOrgnizationDropDownItem", parameters);
        }

        public async Task<JsonResult> GetEmployeeWorkOrganizationalData(long employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            var data = await _dapper.ExecuteStoredProcedureSingle<EmployeeOrgnizationalModel>("usp_GetEmployeeOrganization", parameters);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.Employee + " organization", ActionType.Retrieving));
        }

        public async Task<bool> SaveEmployeeWorkOrganizationalData(EmployeeOrgnizationalModel reqModel)
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
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeOrganization", parameters);
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

        public async Task<bool> SaveEmployeeTimeSheetSetup(TimeSheetSetup timeSheetSetup)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", timeSheetSetup.EmployeeId);
            parameters.Add("@EnableTimeSheet", timeSheetSetup.EnableTimeSheet);
            parameters.Add("@TimeSheetPassword", timeSheetSetup.TimeSheetPassword);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeTimeSheetSetup", parameters);
            return parameters.Get<bool>("@IsSuccess");
        }
        #endregion

        #region Casual Wage
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
        #endregion

        #region Directive
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
        #endregion

        #region TakeOnBalance

        public async Task<List<PayrollTransactionList>> GetPayrollTransactionList(TransactionReqModel reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", reqModel.CompanyId);
            parameters.Add("@AllowanceType", reqModel.AllowanceType);
            parameters.Add("@SearchName", reqModel.SearchName);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            return await _dapper.ExecuteStoredProcedure<PayrollTransactionList>("usp_GetPayrollTransaction", parameters);
        }

        public async Task<bool> SaveEmployeeTakeOnBalance(TransactionSaveModel reqModel)
        {
            var takeOnBalanceTransactionTable = new DataTable();
            takeOnBalanceTransactionTable.Columns.Add("PayrollProcessId", typeof(int));
            takeOnBalanceTransactionTable.Columns.Add("TakeOnBalanceId", typeof(int));
            takeOnBalanceTransactionTable.Columns.Add("Amount", typeof(decimal));
            takeOnBalanceTransactionTable.Columns.Add("Description", typeof(string));
            if (reqModel.PayrollProcess != null)
            {
                foreach (var item in reqModel.PayrollProcess)
                {
                    DataRow row = takeOnBalanceTransactionTable.NewRow();
                    row["PayrollProcessId"] = item.PayrollProcessId ?? (object)DBNull.Value;
                    row["TakeOnBalanceId"] = item.TakeOnBalanceId ?? (object)DBNull.Value;
                    row["Amount"] = item.Amount ?? 0.00m;
                    row["Description"] = item.Description;
                    takeOnBalanceTransactionTable.Rows.Add(row);
                }
            }
            var parameters = new DynamicParameters();
            parameters.Add("@EditMode", reqModel.PayrollProcess?.FirstOrDefault()?.TakeOnBalanceId > 0);
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@AllowanceType", reqModel.AllowanceType);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@PayrollProcess", takeOnBalanceTransactionTable.AsTableValuedParameter("EmployeeTakeOnBalanceTransactionType"));
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeTakeOnBalance", parameters);
        }

        public async Task<TakeOnBalanceListWithTakeOnComplete> GetEmployeeTakeOnBalance(int employeeId, int allowanceType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@AllowanceType", allowanceType);
            parameters.Add("@TakeOnComplete", dbType: DbType.Boolean, direction: ParameterDirection.Output);
            var takeOnBalanceList = await _dapper.ExecuteStoredProcedure<TakeOnBalanceTransaction>("usp_GetEmployeeTakeOnBalance", parameters);
            var takeOnComplete = parameters.Get<bool>("@TakeOnComplete");
            return new TakeOnBalanceListWithTakeOnComplete() { TakeBalanceList = takeOnBalanceList, TakeOnComplete = takeOnComplete };
        }

        public async Task<bool> DeleteEmployeeTakeOnBalance(List<int> takeOnBalanceIds)
        {
            var takeOnBalanceIdTable = new DataTable();
            takeOnBalanceIdTable.Columns.Add("Id", typeof(int));
            foreach (var id in takeOnBalanceIds)
            {
                takeOnBalanceIdTable.Rows.Add(id);
            }
            var parameters = new DynamicParameters();
            parameters.Add("@TakeOnBalanceIds", takeOnBalanceIdTable.AsTableValuedParameter("IntListType"));
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployeeTakeOnBalance", parameters);
        }

        public async Task<bool> SetTakeOnComplete(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@UpdatedBy", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_SetEmployeeTakeOnComplete", parameters);
        }
        #endregion

        #region Loan Info
        public async Task<EmployeeLoanResponse> GetEmployeeLoanDetail(long employeeId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            return await _dapper.ExecuteStoredProcedureMultipleAsync(
                "usp_GetLoanDetailsByEmployeeId",
                parameters,
                async multi =>
                {
                    List<EmployeeLoanInfoDto>? loans = (await multi.ReadAsync<EmployeeLoanInfoDto>()).ToList();
                    LoanSummaryDto? summary = (await multi.ReadAsync<LoanSummaryDto>()).FirstOrDefault() ?? new LoanSummaryDto();

                    return new EmployeeLoanResponse
                    {
                        Loans = loans,
                        Summary = summary
                    };
                }
            );
        }

        public async Task<bool> DeleteEmployeeLoan(int employeeLoanId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@LoanId", employeeLoanId);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteEmployeeLoan", parameters);

            return parameters.Get<bool>("@IsSuccess");
        }
        #endregion

        #region Savings & Garnishee
        public async Task<GarnisheeDropdownListDto> GetGarnisheeDropdownData(long companyId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            return await _dapper.ExecuteStoredProcedureMultipleAsync(
                "usp_GetDropdownDataForGarnishee",
                parameters,
                async multi =>
                {
                    List<AccountTypeDto>? accountTypes = (await multi.ReadAsync<AccountTypeDto>()).ToList();
                    List<BankDto>? banks = (await multi.ReadAsync<BankDto>()).ToList();

                    return new GarnisheeDropdownListDto
                    {
                        AccountTypes = accountTypes,
                        Banks = banks
                    };
                }
            );
        }

        public async Task<bool> UpsertGarnishee(EmployeeGarnisheeRequest request)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@GarnishesId", request.GarnishesId);
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@GarnishesStartDate", request.GarnishesStartDate);
            parameters.Add("@GarnishesAmount", request.GarnishesAmount);
            parameters.Add("@NumberOfRepayment", request.NumberOfRepayment);
            parameters.Add("@CurrentRepayment", request.CurrentRepayment);
            parameters.Add("@IsLinkAccount", request.IsLinkAccount);
            parameters.Add("@AccountName", request.AccountName);
            parameters.Add("@AccountNumber", request.AccountNumber);
            parameters.Add("@AccountTypeId", request.AccountTypeId);
            parameters.Add("@BankId", request.BankId);
            parameters.Add("@BranchCode", request.BranchCode);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertGarnishee", parameters);

            return parameters.Get<bool>("@IsSuccess");
        }

        public async Task<List<GarnishDetails>> GetGarnisheeDetails(long employeeId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            List<GarnishDetails>? result = await _dapper.ExecuteStoredProcedure<GarnishDetails>(
                "usp_GetGarnisheeDetails",
                parameters
            );
            return result;
        }

        public async Task<bool> UpsertSaving(EmployeeSavingsRequest request)
        {
            DynamicParameters? parameters = new DynamicParameters();

            parameters.Add("@SavingsId", request.SavingsId);
            parameters.Add("@EmployeeId", request.EmployeeId);
            parameters.Add("@SavingsStartDate", request.SavingsStartDate);
            parameters.Add("@SavingsAmount", request.SavingsAmount);
            parameters.Add("@NumberOfRepayments", request.NumberOfRepayment);
            parameters.Add("@CurrentRepayment", request.CurrentRepayment);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertSavings", parameters);

            return parameters.Get<bool>("@IsSuccess");
        }

        public async Task<List<SavingsDetails>> GetSavingsDetails(long employeeId)
        {
            DynamicParameters? parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            List<SavingsDetails>? result = await _dapper.ExecuteStoredProcedure<SavingsDetails>(
                "usp_GetSavingsDetails",
                parameters
            );
            return result;
        }
        #endregion

        #region TaxInfo
        public async Task<TaxInformationDropdownData> GetTaxInformationDropdownData()
        {
            return await _dapper.ExecuteStoredProcedureMultipleAsync(
                "usp_GetTaxInformationDropdownData",
                null,
                async multi =>
                {
                    TaxInformationDropdownData? dropdownsData = new TaxInformationDropdownData
                    {
                        TaxMethod = (await multi.ReadAsync<TaxMethod>()).ToList(),
                        IT3aReasonCode = (await multi.ReadAsync<IT3aReasonCode>()).ToList(),
                        UIFExempts = (await multi.ReadAsync<UIFExempts>()).ToList(),
                    };
                    return dropdownsData;
                }
            );
        }

        public async Task<JsonResult> GetTaxInformation(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var result = await _dapper.ExecuteStoredProcedureSingle<TaxInformation>("usp_GetEmployeeTaxInformation", parameters);
            return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, "Tax Information", ActionType.Retrieved));
        }

        public async Task<bool> UpdateTaxInformation(TaxInformation reqModel)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@IncomeTaxNumber", reqModel.IncomeTaxNumber);
            parameters.Add("@TaxOffice", reqModel.TaxOffice);
            parameters.Add("@TaxMethod", reqModel.TaxMethod);
            parameters.Add("@IT3aReasonCodes", reqModel.IT3aReasonCodes);
            parameters.Add("@ExemptFromUIF", reqModel.ExemptFromUIF);
            parameters.Add("@MedicalAidBeneficiaries", reqModel.MedicalAidBeneficiaries);
            parameters.Add("@IsOIDReportExclude", reqModel.IsOIDReportExclude);
            parameters.Add("@IsSDLExempt", reqModel.IsSDLExempt);
            parameters.Add("@IsPrivateBenefit", reqModel.IsPrivateBenefit);
            parameters.Add("@IsCompanyorClose", reqModel.IsCompanyorClose);
            parameters.Add("@IsTrust", reqModel.IsTrust);
            parameters.Add("@IsETIQualifies", reqModel.IsETIQualifies);
            parameters.Add("@MinimumWage", reqModel.MinimumWage);
            parameters.Add("@ValidId", reqModel.ValidId);
            parameters.Add("@IsAverageWorkingHours", reqModel.IsAverageWorkingHours);

            var result = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertTaxInformation", parameters);
            return result;
        }
        #endregion

        #region Documents

        public async Task<List<EmployeeDocuments>> GetEmployeeDocument(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            return await _dapper.ExecuteStoredProcedure<EmployeeDocuments>("usp_GetEmployeeDocument", parameters);
        }

        public async Task<bool> UploadDocumentsSave(List<EmployeeDocuments> employeeDocuments, int employeeId)
        {
            var employeeDocumentTable = new DataTable();
            employeeDocumentTable.Columns.Add("DocumentId", typeof(int));
            employeeDocumentTable.Columns.Add("DocumentName", typeof(string));
            employeeDocumentTable.Columns.Add("DocumentUrl", typeof(string));
            employeeDocumentTable.Columns.Add("DocumentSize", typeof(long));
            employeeDocumentTable.Columns.Add("DocumentType", typeof(string));
            if (employeeDocuments != null)
            {
                foreach (var item in employeeDocuments)
                {
                    DataRow row = employeeDocumentTable.NewRow();
                    row["DocumentId"] = item.DocumentId ?? (object)DBNull.Value;
                    row["DocumentName"] = item.DocumentName;
                    row["DocumentUrl"] = item.DocumentUrl;
                    row["DocumentSize"] = item.DocumentSize;
                    row["DocumentType"] = item.DocumentType;
                    employeeDocumentTable.Rows.Add(row);
                }
            }
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            parameters.Add("@EmployeeDocument", employeeDocumentTable.AsTableValuedParameter("EmployeeDocumentType"));
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpsertEmployeeDocument", parameters);
        }

        public async Task<bool> DeleteDocuments(int documentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@DocumentId", documentId);
            return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteEmployeeDocument", parameters);
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

        public async Task<JsonResult> GetAccessRolesByCompanyId(long companyId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CompanyId", companyId);

            var data = await _dapper.ExecuteStoredProcedure<AccessRoleDto>(
                "usp_GetAccessRolesNamesByCompanyIdForEmployee", parameters);
            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, "Access Roles", ActionType.Retrieved));
        }

        public async Task<JsonResult> UpsertEmployeeUser(UpsertUserRequestDTO request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@AccessRoleId", request.AccessRoleId);
            parameters.Add("@Email", request.Email);
            parameters.Add("@Password", request.Password);
            parameters.Add("@PasswordHash", request.PasswordHash);
            parameters.Add("@CompanyId", request.CompanyId);
            parameters.Add("@EmployeeId", request.EmployeeId);

            var result = await _dapper.ExecuteStoredProcedureSingle<dynamic>("usp_UpsertEmployeeUser", parameters);

            var response = new UpsertUserResponseDTO
            {
                Result = result.Result == 1,
                UserId = result.UserId,
                ErrorNumber = result.ErrorNumber,
                ErrorMessage = result.ErrorMessage
            };

            if (response.Result)
            {
                return HttpStatusCodeResponse.SuccessResponse(response, "User upserted successfully.");
            }
            else
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse($"Error: {response.ErrorMessage} (Error Number: {response.ErrorNumber})");
            }
        }

        #endregion

        public async Task<List<EmployeePreviousService>> GetPreviousService(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);
            return await _dapper.ExecuteStoredProcedure<EmployeePreviousService>("usp_GetPreviousServiceData", parameters);
        }

        public async Task<List<LeaveModel>> GetEmployeeLeaves(int employeeId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", employeeId);

            var data = await _dapper.ExecuteStoredProcedure<LeaveModel>("usp_GetEmployeeLeavesInformation", parameters);
            return data.ToList();
        }

        public async Task<JsonResult> SaveEmpLeaveEntitlementNew(EditLeaveRequest reqModel, string oprType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", reqModel.EmployeeId);
            parameters.Add("@oprType", oprType);
            parameters.Add("@EmployeeLeaveId", reqModel.EmployeeLeaveId ?? (object)DBNull.Value);
            parameters.Add("@Year", reqModel.Year ?? DateTime.Now.Year);
            parameters.Add("@LeaveTypeId", reqModel.LeaveTypeId);
            parameters.Add("@OpeningBalance", reqModel.TakeOnBalance);
            parameters.Add("@DaysAccrued", reqModel.DaysAccrued);
            parameters.Add("@DaysTaken", reqModel.DaysTaken);
            parameters.Add("@DaysDue", reqModel.DaysDue);
            parameters.Add("@LeaveEntitlement", reqModel.CycleLeaveEntitlement);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

            var result = await _dapper.ExecuteStoredProcedure<int>("usp_SaveEmpLeaveEntitlementNew", parameters);
            if (result.Contains(1))
            {
                return HttpStatusCodeResponse.SuccessResponse(
                    string.Empty,
                    string.Format(ResponseMessages.Success, ResponseMessages.LeaveInformation, oprType == "Add" ? ActionType.Saved : ActionType.Updated));
            }
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnexpectedError);
        }

        public async Task<JsonResult> DeleteEmployeeLeave(int employeeLeaveId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeLeaveId", employeeLeaveId);

            var result = await _dapper.ExecuteStoredProcedure<int>("usp_DeleteEmployeeLeaveInformation", parameters);

            return HttpStatusCodeResponse.SuccessResponse(
                string.Empty,
                string.Format(ResponseMessages.Success, ResponseMessages.LeaveInformation, ActionType.Deleted));

        }
    }
}
