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
        try
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetAllDesignations(long companyId)
    {
        try
        {
            var designations = await _designationsRepository.GetAllDesignations(companyId);
            return HttpStatusCodeResponse.SuccessResponse(designations, ResponseMessages.DataFetchSuccess);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> DeleteDesignations(long id)
    {
        try
        {
            await _designationsRepository.DeleteDesignations(id);
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, ResponseMessages.DesignationsDeletedSuccessfully);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> UpdateDesignations(DesignationRequestDTO request)
    {
        try
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
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

public async Task<JsonResult> ImportDesignations(ImportDesignationRequestDTO request)
{
    try
    {
        var errorMessage = await _designationsRepository.ImportDesignations(request);

        if (!string.IsNullOrEmpty(errorMessage))
            return HttpStatusCodeResponse.InternalServerErrorResponse(errorMessage);

        return HttpStatusCodeResponse.SuccessResponse(string.Empty, "Designations imported successfully.");
    }
    catch (Exception)
    {
        return HttpStatusCodeResponse.BadRequestResponse();
    }
}



}
