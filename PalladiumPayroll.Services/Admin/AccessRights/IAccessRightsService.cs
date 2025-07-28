using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;

namespace PalladiumPayroll.Services.Admin.AccessRights;

public interface IAccessRightsService
{
    Task<JsonResult> UpsertAccessRole(AccessRoleDTO request);

    Task<JsonResult> GetAllAccessRoles(int companyId);

    Task<JsonResult> DeleteAccessRole(int accessRoleId);

    Task<JsonResult> GetAccessRightsByRoleType(int accessRoleId);

    Task<JsonResult> SaveRoleFunctinalityAccessRights(List<SaveEmployeeAccessRightsDTO> request);


    // Admin - Pay frequencies 
    Task<JsonResult> GetPayFrequencyAccessRights(int accessRoleId);

    Task<JsonResult> SavePayFrequencyAccessRights(List<SavePayFrequencyAccessRightsDTO> request);


    // Admin - Transaction Functions 
    Task<JsonResult> GetTransactionFunctionAccessRights(int accessRoleId);

}
