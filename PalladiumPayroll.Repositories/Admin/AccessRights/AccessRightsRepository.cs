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

    public AccessRightsRepository(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
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
        foreach (var request in requests)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccessRoleId", request.AccessRoleId);
            parameters.Add("@FunctionalityId", request.FunctionalityId);
            parameters.Add("@View", request.View);
            parameters.Add("@Edit", request.Edit);
            parameters.Add("@Delete", request.Delete);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>(
                "usp_SaveAccessRightsForRoleFunctinality", parameters);

            if (!parameters.Get<bool>("@IsSuccess"))
            {
                return false;
            }
        }

        return true;
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
        foreach (var request in requests)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccessRoleId", request.AccessRoleId);
            parameters.Add("@CompanyPayrollId", request.CompanyPayrollId);
            parameters.Add("@IsAllow", request.IsAllow);
            parameters.Add("@UserId", _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value);
            parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await _dapper.ExecuteStoredProcedureSingle<object>(
                "usp_SaveAccessRightsForPayFrequencies", parameters);

            if (!parameters.Get<bool>("@IsSuccess"))
            {
                return false;
            }
        }

        return true;
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
