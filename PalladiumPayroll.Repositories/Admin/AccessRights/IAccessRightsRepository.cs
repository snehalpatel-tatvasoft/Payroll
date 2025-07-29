using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Repositories.Admin.AccessRights;

public interface IAccessRightsRepository
{
    Task<(bool IsSuccess, int AccessRoleId)> UpsertAccessRole(AccessRoleDTO request);


    Task<List<AccessRoleResponseDTO>> GetAllAccessRoles(long companyId);

    Task<bool> CheckAccessRoleExists(string accessRoleName, long companyId, int? accessRoleId = null);

    Task<bool> DeleteAccessRoles(int accessRoleId);

    Task<List<AccessRightsByRoleTypeDTO>> GetAccessRightsByRoleType(int accessRoleId);

    Task<bool> SaveRoleFunctinalityAccessRights(List<SaveRoleFunctinalityAccessRightsDTO> requests);


    Task<bool> SavePayFrequencyAccessRights(List<SavePayFrequencyAccessRightsDTO> requests);

    Task<List<PayFrequencyAccessRightsDTO>> GetPayFrequencyAccessRights(int accessRoleId);

     Task<List<AccessRightsByRoleTypeDTO>> GetTransactionFunctionAccessRights(int accessRoleId);
}
