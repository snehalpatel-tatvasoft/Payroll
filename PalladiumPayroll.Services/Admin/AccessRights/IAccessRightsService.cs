using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Services.Admin.AccessRights;

public interface IAccessRightsService
{
    Task<JsonResult> UpsertAccessRole(AccessRoleDTO request);

    Task<JsonResult> GetAllAccessRoles(int companyId);

    Task<JsonResult> DeleteAccessRole(int accessRoleId);
}
