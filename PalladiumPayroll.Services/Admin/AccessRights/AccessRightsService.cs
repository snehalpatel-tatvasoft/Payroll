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
        bool isExists = await _accessRightsRepository.CheckAccessRoleExists(request.AccessRoleName, request.CompanyId, request.AccessRoleId);

        if (isExists)
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.AcceessRoleAlreadyExists);

        var (isSaved, accessRoleId) = await _accessRightsRepository.UpsertAccessRole(request);

        return isSaved
            ? HttpStatusCodeResponse.SuccessResponse(new { accessRoleId }, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Saved))
            : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRole);
    }

    public async Task<JsonResult> GetAllAccessRoles(int companyId)
    {
        List<AccessRoleResponseDTO> accessRoles = await _accessRightsRepository.GetAllAccessRoles(companyId);
        return HttpStatusCodeResponse.SuccessResponse(accessRoles, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Retrieved));

    }

    public async Task<JsonResult> DeleteAccessRole(int accessRoleId)
    {
        bool isDeleted = await _accessRightsRepository.DeleteAccessRoles(accessRoleId);

        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.MinimumWageNotFound);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Deleted));
    }

    public async Task<JsonResult> GetAccessRightsByRoleType(int accessRoleId)
    {
        List<AccessRightsByRoleTypeDTO> employeeAccessRights = await _accessRightsRepository.GetAccessRightsByRoleType(accessRoleId);
        return HttpStatusCodeResponse.SuccessResponse(employeeAccessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
    }


    public async Task<JsonResult> SaveRoleFunctinalityAccessRights(List<SaveRoleFunctinalityAccessRightsDTO> request)
    {
        bool isSaved = await _accessRightsRepository.SaveRoleFunctinalityAccessRights(request);
        return isSaved
            ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
            : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
    }

    public async Task<JsonResult> GetPayFrequencyAccessRights(int accessRoleId)
    {
        List<PayFrequencyAccessRightsDTO> accessRights = await _accessRightsRepository.GetPayFrequencyAccessRights(accessRoleId);
        return HttpStatusCodeResponse.SuccessResponse(accessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
    }

    public async Task<JsonResult> SavePayFrequencyAccessRights(List<SavePayFrequencyAccessRightsDTO> request)
    {
        bool isSaved = await _accessRightsRepository.SavePayFrequencyAccessRights(request);
        return isSaved
            ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
            : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
    }

    public async Task<JsonResult> GetTransactionFunctionAccessRights(int accessRoleId)
    {
        List<AccessRightsByRoleTypeDTO> employeeAccessRights = await _accessRightsRepository.GetTransactionFunctionAccessRights(accessRoleId);
        return HttpStatusCodeResponse.SuccessResponse(employeeAccessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
    }
}
