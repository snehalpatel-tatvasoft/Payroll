using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeeGrievances;

namespace PalladiumPayroll.Repositories.HRFunctions.EmployeeGrievances;

public class EmployeeGrievancesRepository : IEmployeeGrievancesRepository
{
    private readonly DapperContext _dapper;

    public EmployeeGrievancesRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }


    public async Task<bool> UpsertEmployeeGrievance(EmployeeGrievanceUpsertData request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@EmployeeGrievanceId", request.EmployeeGrievanceId);
        parameters.Add("@EmployeeId", request.EmployeeId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@Date", request.Date);
        parameters.Add("@Accused", request.Accused);
        parameters.Add("@NatureOfGrievanceId", request.NatureOfGrievanceId);
        parameters.Add("@Stage1Date", request.Stage1Date);
        parameters.Add("@Stage1Outcome", request.Stage1Outcome);
        parameters.Add("@Stage1Chairperson", request.Stage1Chairperson);
        parameters.Add("@Stage2Date", request.Stage2Date);
        parameters.Add("@Stage2Outcome", request.Stage2Outcome);
        parameters.Add("@Stage2Chairperson", request.Stage2Chairperson);
        parameters.Add("@UserId", request.UserId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertEmployeeGrievance", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<bool> DeleteEmployeeGrievance(long employeeGrievanceId, string userId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@EmployeeGrievanceId", employeeGrievanceId);
        parameters.Add("@UserId", userId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteEmployeeGrievance", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }

    public async Task<List<EmployeeDropdownDTO>> GetEmployeesForGrievances(long companyId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<EmployeeDropdownDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeDropdownDTO>(
            "usp_GetEmployeesForEmployeeGrievances",
            parameters
        );

        return result ?? new List<EmployeeDropdownDTO>();
    }

    public async Task<List<NatureOfGrievanceDTO>> GetNatureOfGrievances(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<NatureOfGrievanceDTO>? result = await _dapper.ExecuteStoredProcedure<NatureOfGrievanceDTO>(
            "usp_GetNatureOfGrievances",
            parameters
        );

        return result ?? new List<NatureOfGrievanceDTO>();
    }


    public async Task<List<EmployeeGrievanceDTO>> GetEmployeeGrievances(long companyId)
    {
         DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<EmployeeGrievanceDTO>? result = await _dapper.ExecuteStoredProcedure<EmployeeGrievanceDTO>(
            "usp_GetEmployeeGrievances",
            parameters
        );

        return result;
    }



}
