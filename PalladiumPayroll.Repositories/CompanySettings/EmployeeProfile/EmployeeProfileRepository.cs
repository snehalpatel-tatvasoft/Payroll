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

namespace PalladiumPayroll.Repositories.CompanySettings.EmployeeProfile;

public class EmployeeProfileRepository : IEmployeeProfileRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;

    public EmployeeProfileRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
    }
    public async Task<(string Message, int EmployeeProfileId)> CreateProfile(EmployeeProfileRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", request.Name);
        parameters.Add("@CompanyID", request.CompanyId);
        parameters.Add("@EmployeeProfileId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        parameters.Add("@ErrorMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);

        await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CreateEmployeeProfile", parameters);

        var message = parameters.Get<string>("@ErrorMessage");
        var employeeProfileId = parameters.Get<int>("@EmployeeProfileId");

        return (message, employeeProfileId);
    }
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

        parameters.Add("@Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteAsync("usp_SaveCompanyWorkInformation", parameters);

        return parameters.Get<bool>("@Result");
    }
    public async Task<List<TransactionListModel>> GetModalTransactionsList(int transactionId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);

        var transactions = await _dapper.ExecuteStoredProcedure<TransactionListModel>(
            "usp_GetModalTransactionsList",
            parameters
        );

        return transactions ?? new List<TransactionListModel>();
    }
    public async Task<List<TransactionListModel>> GetTransactionsList(int transactionId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TransactionId", transactionId);

        var transactions = await _dapper.ExecuteStoredProcedure<TransactionListModel>(
            "usp_GetTransactionsList",
            parameters
        );

        return transactions ?? new List<TransactionListModel>();
    }
    

}
