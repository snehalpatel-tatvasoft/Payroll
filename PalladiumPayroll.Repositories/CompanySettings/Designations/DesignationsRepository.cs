using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs;
using System.Data;
using System.Net;

namespace PalladiumPayroll.Repositories.Comany_Settings;

public class DesignationsRepository : IDesignationsRepository
{
    private readonly DapperContext _dapper;
    private readonly IConfiguration _configuration;

    public DesignationsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dapper = new DapperContext(_configuration);
    }

    public async Task<bool> CreateDesignations(DesignationRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@DesignationName", request.DesignationsName);
        parameters.Add("@DesignationCode", request.DesignationsCode);
        parameters.Add("@ComanyID", request.CompanyId);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_CreateDesignation", parameters);
    }

    public async Task<List<DesignationResponseDTO>> GetAllDesignations(long companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        var result = await _dapper.ExecuteStoredProcedure<DesignationResponseDTO>(
            "usp_GetAllDesignations", parameters);
        return result.ToList();
    }

    public async Task<(bool isSuccess, string message)> DeleteDesignations(long designationId, long? employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@DesignationId", designationId);
        parameters.Add("@EmployeeId", employeeId);
        parameters.Add("@ResultMessage", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

        await _dapper.ExecuteAsync(
            "usp_DeleteDesignation",
            parameters
        );

        string message = parameters.Get<string>("@ResultMessage");

        bool success = message.Contains("successfully", StringComparison.OrdinalIgnoreCase);

        return (success, message);
    }

    public async Task<bool> UpdateDesignations(DesignationRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", request.Id);
        parameters.Add("@DesignationName", request.DesignationsName);
        parameters.Add("@DesignationCode", request.DesignationsCode);
        parameters.Add("@ComanyID", request.CompanyId);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_UpdateDesignation", parameters);
    }
    public async Task<bool> CheckDuplicateDesignation(DesignationRequestDTO request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", request.Id);
        parameters.Add("@DesignationsName", request.DesignationsName);
        parameters.Add("@DesignationsCode", request.DesignationsCode);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@IsDuplicate", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedure<object>("usp_CheckDuplicateDesignation", parameters);
        return parameters.Get<bool>("@IsDuplicate");
    }

    public async Task<string?> ImportDesignations(ImportDesignationRequestDTO request)
    {
        var table = DesignationsToDataTable(request.Designations);

        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@Designations", table.AsTableValuedParameter("dbo.DesignationImportType"));

        var result = await _dapper.ExecuteStoredProcedureSingle<string>(
            "usp_ImportDesignations", parameters
        );

        if (result == "DUPLICATE")
            return "Duplicate record";
        if (result == "DUPLICATE_CODE")
            return "Code already exists for another designation";

        return result;
    }

    private DataTable DesignationsToDataTable(List<DesignationRequestDTO> data)
    {
        var table = new DataTable();
        table.Columns.Add("DesignationName", typeof(string));
        table.Columns.Add("DesignationCode", typeof(string));

        foreach (var item in data)
        {
            table.Rows.Add(
                item.DesignationsName ?? (object)DBNull.Value,
                item.DesignationsCode ?? (object)DBNull.Value
            );
        }

        return table;
    }

}
