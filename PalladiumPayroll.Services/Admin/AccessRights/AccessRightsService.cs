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
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.AcceessRoleAlreadyExists);

            var (isSaved, accessRoleId) = await _accessRightsRepository.UpsertAccessRole(request);

            return isSaved
                ? HttpStatusCodeResponse.SuccessResponse(new { accessRoleId }, string.Format(ResponseMessages.Success, ResponseMessages.AccessRole, ActionType.Saved))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRole);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
         string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.AccessRole, ex.Message)
     );
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

    public async Task<JsonResult> GetAccessRightsByRoleType(int accessRoleId)
    {
        try
        {
            List<EmployeeAccessRightsDTO> employeeAccessRights = await _accessRightsRepository.GetAccessRightsByRoleType(accessRoleId);

            return HttpStatusCodeResponse.SuccessResponse(employeeAccessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> SaveRoleFunctinalityAccessRights(List<SaveEmployeeAccessRightsDTO> request)
    {
        try
        {
            bool isSaved = await _accessRightsRepository.SaveRoleFunctinalityAccessRights(request);
            return isSaved
                ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetPayFrequencyAccessRights(int accessRoleId)
    {
        try
        {
            List<PayFrequencyAccessRightsDTO> accessRights = await _accessRightsRepository.GetPayFrequencyAccessRights(accessRoleId);

            return HttpStatusCodeResponse.SuccessResponse(accessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> SavePayFrequencyAccessRights(List<SavePayFrequencyAccessRightsDTO> request)
    {
        try
        {
            bool isSaved = await _accessRightsRepository.SavePayFrequencyAccessRights(request);
            return isSaved
                ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Saved))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveAccessRights);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetTransactionFunctionAccessRights(int accessRoleId)
    {
        try
        {
            List<EmployeeAccessRightsDTO> employeeAccessRights = await _accessRightsRepository.GetTransactionFunctionAccessRights(accessRoleId);

            return HttpStatusCodeResponse.SuccessResponse(employeeAccessRights, string.Format(ResponseMessages.Success, ResponseMessages.AccessRights, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


}
