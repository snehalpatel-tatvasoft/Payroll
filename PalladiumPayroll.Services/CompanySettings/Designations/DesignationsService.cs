using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.Company_Settings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.Comany_Settings;
using static PalladiumPayroll.Helper.Constants.AppConstants;

namespace PalladiumPayroll.Services.Company_Settings;

public class DesignationsService : IDesignationsService
{
    private readonly IDesignationsRepository _designationsRepository;

    public DesignationsService(IDesignationsRepository designationsRepository)
    {
        _designationsRepository = designationsRepository;
    }

    public async Task<JsonResult> CreateDesignations(DesignationRequestDTO request)
    {
        bool isDuplicate = await _designationsRepository.CheckDuplicateDesignation(request);

        if (isDuplicate)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DesignationDuplicate);
        }

        bool isCreated = await _designationsRepository.CreateDesignations(request);
        if (isCreated)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.DesignationsCreatedSuccessfully);

        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DesignationsCreationFailed);
    }

    public async Task<JsonResult> GetAllDesignations(long companyId)
    {

        var designations = await _designationsRepository.GetAllDesignations(companyId);
        return HttpStatusCodeResponse.SuccessResponse(designations, ResponseMessages.DataFetchSuccess);
    }

    public async Task<JsonResult> DeleteDesignations(long designationId, long? employeeId)
    {
        try
        {
            var (isSuccess, message) = await _designationsRepository.DeleteDesignations(designationId, employeeId);
            if (!isSuccess)
            {
                return HttpStatusCodeResponse.InternalServerErrorResponse(message);
            }

            return HttpStatusCodeResponse.SuccessResponse(
                data: string.Empty,
                ResponseMessages.DesignationsDeletedSuccessfully
            );
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                ResponseMessages.ErrorDeletingDesignations
            );
        }

    }

    public async Task<JsonResult> UpdateDesignations(DesignationRequestDTO request)
    {

        bool isDuplicate = await _designationsRepository.CheckDuplicateDesignation(request);

        if (isDuplicate)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DesignationDuplicate);
        }
        bool isUpdated = await _designationsRepository.UpdateDesignations(request);
        if (isUpdated)
        {
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.DesignationsUpdatedSuccessfully);
        }

        return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DesignationsUpdateFailed);

    }

    public async Task<JsonResult> ImportDesignations(ImportDesignationRequestDTO request)
    {

        var errorMessage = await _designationsRepository.ImportDesignations(request);
        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.DesignationsImportedSuccessfully);
    }

}
