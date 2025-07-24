using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.Admin.AccessRights;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Admin.AccessRights;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.Admin.AccessRights;

public class AccessRightsService : IAccessRightsService
{
    private readonly IAccessRightsRepository _accessRightsRepository;

    public AccessRightsService(IAccessRightsRepository accessRightsRepository)
    {
        _accessRightsRepository = accessRightsRepository;
    }

    public async Task<JsonResult> UpsertAccessRole(AccessRoleDTO request)
    {
        try
        {
            bool isExists = await _accessRightsRepository.CheckAccessRoleExists(request.AccessRoleName, request.CompanyId, request.AccessRoleId);

            if (isExists)
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DuplicateMinimumWage);

            bool isSaved = await _accessRightsRepository.UpsertAccessRole(request);
            return isSaved
                ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Created))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRole);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> GetAllAccessRoles(int companyId)
    {
        try
        {
            List<AccessRoleResponseDTO> accessRoles = await _accessRightsRepository.GetAllAccessRoles(companyId);

            return HttpStatusCodeResponse.SuccessResponse(accessRoles, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> DeleteAccessRole(int accessRoleId)
    {
        try
        {
            bool isDeleted = await _accessRightsRepository.DeleteAccessRoles(accessRoleId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.MinimumWageNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Deleted));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }

    }

}
