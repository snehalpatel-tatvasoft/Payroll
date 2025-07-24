using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.Admin.AccessRights;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.Admin;

[ApiController]
[Route("api/[controller]")]
public class AccessRightsController : ControllerBase
{
    private readonly IAccessRightsService _accessRightsService;

    public AccessRightsController(IAccessRightsService accessRightsService)
    {
        _accessRightsService = accessRightsService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UpsertAccessRole(AccessRoleDTO request)
    {
        try
        {
            return await _accessRightsService.UpsertAccessRole(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.AccessRole, ex.Message)
            );
        }
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetAllAccessRoles(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _accessRightsService.GetAllAccessRoles(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.AccessRole, ex.Message));
        }
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteAccessRole(int accessRoleId)
    {
        try
        {
            if (accessRoleId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidAccessRoleId);
            }

            return await _accessRightsService.DeleteAccessRole(accessRoleId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.AccessRole, ex.Message)
            );
        }
    }

}
