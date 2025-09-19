using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using Dapper;
using System.Data;
using PalladiumPayroll.DTOs.DTOs.CompanySettings.EmployeeProfile;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Common;
using PalladiumPayroll.DTOs.Miscellaneous;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;
using Microsoft.AspNetCore.Http;

namespace PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

public class EmployeeProfileRepository : IEmployeeProfileRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public EmployeeProfileRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    #region Profile

    public async Task<(string Message, int EmployeeProfileId)> CreateProfile(EmployeeProfileRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeProfileId", request.Id, DbType.Int32, ParameterDirection.InputOutput);
        parameters.Add("@Name", request.Name);
        parameters.Add("@CompanyID", request.CompanyId);
        parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

        await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CreateOrUpdateEmployeeProfile", parameters);

        var message = parameters.Get<string>("@ErrorMessage");
        var employeeProfileId = parameters.Get<int>("@EmployeeProfileId");

        return (message, employeeProfileId);
    }

    public async Task<List<EmployeeProfileListDTO>> GetAllEmployeeProfiles(int companyId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<EmployeeProfileListDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeProfileListDTO>(
            "usp_GetAllEmployeeProfiles",
            parameters
        );

        return result;
    }

    public async Task<(bool isSuccess, string message)> DeleteEmployeeProfile(long profileId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@ProfileId", profileId);
        parameters.Add("@ResultMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

        await _dapper.ExecuteAsync(
            "usp_DeleteEmployeeProfile",
            parameters
        );

        string message = parameters.Get<string>("@ResultMessage");

        bool success = message.Contains("successfully", StringComparison.OrdinalIgnoreCase);

        return (success, message);
    }

    #endregion


    #region work information
    public async Task<JsonResult> GetWorkInformatiionDropdownData(int companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        var result = await _dapper.ExecuteStoredProcedureMultipleAsync("usp_FetchCompanyWorkInfoDropList", parameters, async (multi) =>
        {
            var cycleList = (await multi.ReadAsync<DropDownViewModel>()).ToList();
            return new
            {
                CycleList = cycleList,
            };
        });
        return HttpStatusCodeResponse.SuccessResponse(result, string.Format(ResponseMessages.Success, ResponseMessages.Company + "work information drop list", ActionType.Retrieved));
    }
    public async Task<bool> SaveWorkInformation(WorkInformationRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ProfileId", request.ProfileId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@DepartmentId", request.DepartmentId);
        parameters.Add("@DesignationId", request.DesignationId);
        parameters.Add("@CycleTypeId", request.CycleTypeId);
        parameters.Add("@AnnualSalary", request.AnnualSalary);
        parameters.Add("@MonthlySalary", request.MonthlySalary);
        parameters.Add("@RatePerDay", request.RatePerDay);
        parameters.Add("@RatePerHour", request.RatePerHour);
        parameters.Add("@WorkingDays", string.Join(",", request.WorkingDays), DbType.String);
        parameters.Add("@HoursPerMonth", request.HoursPerMonth);
        parameters.Add("@HoursPerWeek", request.HoursPerWeek);
        parameters.Add("@HoursPerDay", request.HoursPerDay);
        parameters.Add("@DayPerMonth", request.DayPerMonth);
        parameters.Add("@DayPerWeek", request.DayPerWeek);
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);

        parameters.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteAsync("usp_SaveCompanyWorkInformation", parameters);

        return parameters.Get<bool>("@Result");
    }

    public async Task<EmployeeProfileDetailsDTO?> GetEmployeeProfileDetailsById(long profileId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@ProfileId", profileId);

        return await _dapper.ExecuteStoredProcedureSingle<EmployeeProfileDetailsDTO>(
            "usp_GetEmployeeProfileDetailsByProfileId", parameters);
    }

    #endregion


    #region Leave Settings

    public async Task<List<LeaveRulesListDTO>> GetLeaveRulesForEmployeeProfile(int companyId, int caseId, long profileId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@CaseId", caseId);
        parameters.Add("@ProfileId", profileId);

        return await _dapper.ExecuteStoredProcedure<LeaveRulesListDTO>("usp_GetLeaveRulesInEmployeeProfile", parameters);
    }

    public async Task<bool> UpdateLeaveSettingsInEmployeeProfile(LeaveSettingsUpdateRequestDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@LeaveRulePerProfileId", request.LeaveRuleId);
        parameters.Add("@CaseId", request.CaseId);
        parameters.Add("@Duration", request.Duration);
        parameters.Add("@LeaveAccumulationDays", request.LeaveAccumulationDays);
        parameters.Add("@ExceedDue", request.ExceedDue);
        parameters.Add("@LeaveCarriedForward", request.LeaveCarriedForward);
        parameters.Add("@LeaveCarriedForwardMaxDays", request.LeaveCarriedForwardMaxDays);
        parameters.Add("@Recurring", request.Recurring);
        parameters.Add("@NoOfTimeReccuring", request.NoOfTimeReccuring);
        parameters.Add("@AnnualEntitlementDays", request.AnnualEntitlementDays);

        bool isSuccess = await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateLeaveRulesInEmployeeProfile", parameters);
        return isSuccess;
    }

    #endregion
    #region  Transaction
    public async Task<List<TransactionListModel>> GetModalTransactionsList(int transactionId,int companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);
        parameters.Add("@CompanyId", companyId);


        var transactions = await _dapper.ExecuteStoredProcedure<TransactionListModel>(
            "usp_GetModalTransactionsList",
            parameters
        );

        return transactions ?? new List<TransactionListModel>();
    }
    public async Task<List<TransactionListModel>> GetTransactionsList(int transactionId, int companyId, int profileId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@ProfileId", profileId);



        var transactions = await _dapper.ExecuteStoredProcedure<TransactionListModel>(
            "usp_GetProfileTransactionDetails",
            parameters
        );

        return transactions ?? new List<TransactionListModel>();
    }
    public async Task<bool> SaveTransactionAssignments(SaveTransactionAssignmentsRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@ProfileId", request.ProfileId);
        parameters.Add("@TransactionId", request.TransactionId); // if needed for grouping/filter
        parameters.Add("@PayrollProcessIds", string.Join(",", request.PayrollProcessId)); // pass as CSV
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteAsync("usp_SaveTransactionAssignments", parameters);

        return parameters.Get<bool>("@Result");
    }
    public async Task<bool> DeleteTransactionAssignments(List<long> ids)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Ids", string.Join(",", ids));

        var result = await _dapper.ExecuteStoredProcedure<bool>(
            "usp_DeleteProfileTransactionDetails",
            parameters
        );

        return result.FirstOrDefault(); // true if deleted successfully
    }


    #endregion

}
