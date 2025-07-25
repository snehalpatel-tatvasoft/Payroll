using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Repositories.Admin.AccessRights;

public class AccessRightsRepository : IAccessRightsRepository
{
    private readonly DapperContext _dapper;

    public AccessRightsRepository(IConfiguration configuration)
    {
        _dapper = new DapperContext(configuration);
    }

    public async Task<bool> UpsertAccessRole(AccessRoleDTO request)
    {
        DynamicParameters? parameters = new DynamicParameters();

        parameters.Add("@AccessRoleId", request.AccessRoleId);
        parameters.Add("@AccessRoleName", request.AccessRoleName);
        parameters.Add("@AccessTypeId", request.AccessTypeId);
        parameters.Add("@CompanyId", request.CompanyId);
        parameters.Add("@UserId", request.UserId);
        parameters.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

        await _dapper.ExecuteStoredProcedureSingle<object>("usp_UpsertAccessRole", parameters);

        return parameters.Get<bool>("@IsSuccess");
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

}
