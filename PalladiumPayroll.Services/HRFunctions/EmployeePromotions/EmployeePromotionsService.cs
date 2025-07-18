using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.HRFunctions.EmployeePromotions;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Services.HRFunctions.EmployeePromotions;

public class EmployeePromotionsService : IEmployeePromotionsService
{
    private readonly IEmployeePromotionsRepository _employeePromotionsRepository;

    public EmployeePromotionsService(IEmployeePromotionsRepository employeePromotionsRepository)
    {
        _employeePromotionsRepository = employeePromotionsRepository;
    }

    public async Task<JsonResult> UpsertEmployeePromotions(EmployeePromotionsUpsertData request)
    {
        try
        {
            bool isSaved = await _employeePromotionsRepository.UpsertEmployeePromotions(request);

            if (!isSaved)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeGrievanceSaveFailed);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Saved));

        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> DeleteEmployeePromotion(long employeePromotionId, string userId)
    {
        try
        {
            bool isDeleted = await _employeePromotionsRepository.DeleteEmployeePromotion(employeePromotionId, userId);

            if (!isDeleted)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeePromotionNotFound);
            }
            return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Deleted));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeePromotionDropdownData(long companyId)
    {
        try
        {
            EmployeePromotionDropdownsDTO? data = await _employeePromotionsRepository.GetEmployeePromotionDropdownData(companyId);

            return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeePromotions(long companyId)
    {
        try
        {
            List<EmployeePromotionsdisplayDataDTO> employeePromotions = await _employeePromotionsRepository.GetEmployeePromotionsDisplayData(companyId);

            return HttpStatusCodeResponse.SuccessResponse(employeePromotions, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Retrieved));
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }

    public async Task<JsonResult> GetEmployeePromotionById(long promotionId)
    {
        try
        {
            EmployeePromotionDetailDTO? employeePromotion = await _employeePromotionsRepository.GetEmployeePromotioneById(promotionId);

            return HttpStatusCodeResponse.SuccessResponse(employeePromotion, string.Empty);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.BadRequestResponse();
        }
    }
}
