using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Repositories.Admin.AccessRights;

public interface IAccessRightsRepository
{
    Task<bool> UpsertAccessRole(AccessRoleDTO request);
    
    Task<List<AccessRoleResponseDTO>> GetAllAccessRoles(long companyId);

    Task<bool> CheckAccessRoleExists(string accessRoleName, long companyId, int? accessRoleId = null);

    Task<bool> DeleteAccessRoles(int accessRoleId);
}
