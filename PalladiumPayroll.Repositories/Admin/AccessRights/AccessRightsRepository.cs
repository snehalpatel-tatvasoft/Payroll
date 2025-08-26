using System.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Repositories.Admin.AccessRights;

public class AccessRightsRepository : IAccessRightsRepository
{
    private readonly DapperContext _dapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessRightsRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _dapper = new DapperContext(configuration);
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(bool IsSuccess, int AccessRoleId)> UpsertAccessRole(AccessRoleDTO request)
    {
        DynamicParameters parameters = new DynamicParameters();

        parameters.Add("@AccessRoleId", request.AccessRoleId, DbType.Int32, ParameterDirection.InputOutput);
        parameters.Add("@AccessRoleName", request.AccessRoleName);
        parameters.Add("@AccessTypeId", request.AccessTypeId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertAccessRole", parameters);

        bool isSuccess = parameters.Get<bool>("@IsSuccess");
        int updatedAccessRoleId = parameters.Get<int>("@AccessRoleId");

        return (isSuccess, updatedAccessRoleId);
    }


    public async Task<List<AccessRoleResponseDTO>> GetAllAccessRoles(long companyId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);

        List<AccessRoleResponseDTO>? result = await _dapper.ExecuteStoredProcedure<AccessRoleResponseDTO>(
            "usp_GetAllAccessRole",
            parameters
        );

        return result;
    }

    public async Task<bool> CheckAccessRoleExists(string accessRoleName, long companyId, int? accessRoleId = null)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId);
        parameters.Add("@AccessRoleName", accessRoleName);
        parameters.Add("@AccessRoleID", accessRoleId);
        parameters.Add("@IsExists", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_CheckAccessRoleExists", parameters);
        return parameters.Get<bool>("@IsExists");
    }


    public async Task<bool> DeleteAccessRoles(int accessRoleId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@AccessRoleId", accessRoleId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_DeleteAccessRole", parameters);
        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<List<AccessRightsByRoleTypeDTO>> GetAccessRightsByRoleType(int accessRoleId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@AccessRoleId", accessRoleId);

        List<AccessRightsByRoleTypeDTO>? result = await _dapper.ExecuteStoredProcedure<AccessRightsByRoleTypeDTO>(
           "usp_GetAccessRightsByRoleType", parameters);

        return result;
    }

    public async Task<bool> SaveRoleFunctinalityAccessRights(List<SaveRoleFunctinalityAccessRightsDTO> requests)
    {
        DataTable? table = new DataTable();
        table.Columns.Add("AccessRoleId", typeof(int));
        table.Columns.Add("FunctionalityId", typeof(int));
        table.Columns.Add("View", typeof(bool));
        table.Columns.Add("Edit", typeof(bool));
        table.Columns.Add("Delete", typeof(bool));

        foreach (var request in requests)
        {
            table.Rows.Add(request.AccessRoleId, request.FunctionalityId, request.View, request.Edit, request.Delete);
        }

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@AccessRights", table.AsTableValuedParameter("dbo.RoleFunctionalityAccessRightsType"));
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>(
            "usp_SaveAccessRightsForRoleFunctinality", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }


    public async Task<List<PayFrequencyAccessRightsDTO>> GetPayFrequencyAccessRights(int accessRoleId)
    {
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@AccessRoleId", accessRoleId);

        var result = await _dapper.ExecuteStoredProcedure<PayFrequencyAccessRightsDTO>(
            "usp_GetAccessRightsForPayFrequencies", parameters);

        return result ?? new List<PayFrequencyAccessRightsDTO>();
    }

    public async Task<bool> SavePayFrequencyAccessRights(List<SavePayFrequencyAccessRightsDTO> requests)
    {
        DataTable? table = new DataTable();
        table.Columns.Add("AccessRoleId", typeof(int));
        table.Columns.Add("CompanyPayrollId", typeof(long));
        table.Columns.Add("IsAllow", typeof(bool));

        foreach (var request in requests)
        {
            table.Rows.Add(request.AccessRoleId, request.CompanyPayrollId, request.IsAllow);
        }

        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@AccessRights", table.AsTableValuedParameter("dbo.PayFrequencyAccessRightsType"));
        parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>(
            "usp_SaveAccessRightsForPayFrequencies", parameters);

        return parameters.Get<bool>("@IsSuccess");
    }



    public async Task<List<AccessRightsByRoleTypeDTO>> GetTransactionFunctionAccessRights(int accessRoleId)
    {
        DynamicParameters? parameters = new DynamicParameters();
        parameters.Add("@AccessRoleId", accessRoleId);

        List<AccessRightsByRoleTypeDTO>? result = await _dapper.ExecuteStoredProcedure<AccessRightsByRoleTypeDTO>(
            "usp_GetAccessRightsForTransactionFunctions", parameters);

        return result;
    }


}
