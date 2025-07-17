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


    public async Task<bool> DeleteDesignations(long id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return await _dapper.ExecuteStoredProcedureSingle<bool>("usp_DeleteDesignation", parameters);
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
    foreach (var item in request.Designations)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@DesignationsName", item.DesignationsName);
        parameters.Add("@DesignationsCode", item.DesignationsCode);
        parameters.Add("@CompanyId", request.CompanyId);

        var result = await _dapper.ExecuteStoredProcedureSingle<string>("usp_ImportDesignations", parameters);

        if (result == "DUPLICATE")
            return $"Duplicate record: {item.DesignationsName} - {item.DesignationsCode}";

        if (result == "DUPLICATE_CODE")
            return $"Code already exists for another designation: {item.DesignationsCode}";
    }

    return null; 
}


}
