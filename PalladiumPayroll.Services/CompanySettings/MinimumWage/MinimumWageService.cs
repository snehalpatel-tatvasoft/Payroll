using Azure;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.DTOs.ResponseDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.CompanySettings;

public class MinimumWageService : IMinimumWageService
{
    private readonly IMinimumWageRepository _minimumWageRepository;

    public MinimumWageService(IMinimumWageRepository minimumWageRepository)
    {
        _minimumWageRepository = minimumWageRepository;
    }

    public async Task<JsonResult> CreateMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            bool isDuplicate = await _minimumWageRepository.IsDuplicateMinimumWageName(request.Name, request.CompanyId);
            if (isDuplicate)
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DuplicateMinimumWage);

            bool isSaved = await _minimumWageRepository.CreateMinimumWage(request);
            return isSaved
                ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Created))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.UnableToSaveMinimumWage);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> GetMinimumWagesByCompanyId(int companyId)
    {
        try
        {
            List<MinimumWageResponseDTO> wages = await _minimumWageRepository.GetMinimumWagesByCompanyId(companyId);

            return HttpStatusCodeResponse.SuccessResponse(wages, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> UpdateMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            bool isDuplicate = await _minimumWageRepository.IsDuplicateMinimumWageName(request.Name, request.CompanyId, request.Id);
            if (isDuplicate)
                return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.DuplicateMinimumWage);

            bool isUpdated = await _minimumWageRepository.UpdateMinimumWage(request);

            return isUpdated
                ? HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Updated))
                : HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.MinimumWageNotFound);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }


    public async Task<JsonResult> DeleteMinimumWage(int wageId)
    {
        try
        {
            bool isDeleted = await _minimumWageRepository.DeleteMinimumWage(wageId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.MinimumWageNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.MinimumWage, ActionType.Deleted));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }

    }
}
