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

    public async Task<JsonResult> AddEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        bool isSaved = await _employeePromotionsRepository.AddEmployeePromotion(request);
        if (!isSaved)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(ResponseMessages.EmployeeGrievanceSaveFailed);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Saved));
    }

    public async Task<JsonResult> UpdateEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        bool isSaved = await _employeePromotionsRepository.UpdateEmployeePromotion(request);
        if (!isSaved)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeeGrievanceSaveFailed);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Updated));
    }

    public async Task<JsonResult> DeleteEmployeePromotion(long employeePromotionId)
    {
        bool isDeleted = await _employeePromotionsRepository.DeleteEmployeePromotion(employeePromotionId);

        if (!isDeleted)
        {
            return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.EmployeePromotionNotFound);
        }
        return HttpStatusCodeResponse.SuccessResponse(string.Empty, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Deleted));
    }

    public async Task<JsonResult> GetEmployeePromotionDropdownData(long companyId)
    {
        EmployeePromotionDropdownsDTO? data = await _employeePromotionsRepository.GetEmployeePromotionDropdownData(companyId);
        return HttpStatusCodeResponse.SuccessResponse(data, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeePromotions(long companyId)
    {
        List<EmployeePromotionsdisplayDataDTO> employeePromotions = await _employeePromotionsRepository.GetEmployeePromotionsDisplayData(companyId);
        return HttpStatusCodeResponse.SuccessResponse(employeePromotions, string.Format(ResponseMessages.Success, ResponseMessages.EmployeePromotions, ActionType.Retrieved));
    }

    public async Task<JsonResult> GetEmployeePromotionById(long promotionId)
    {
        EmployeePromotionDetailDTO? employeePromotion = await _employeePromotionsRepository.GetEmployeePromotioneById(promotionId);
        return HttpStatusCodeResponse.SuccessResponse(employeePromotion, string.Empty);
    }

    public async Task<JsonResult> GetEmployeePromotionAutofillData(long employeeId, long companyId)
    {
        EmployeePromotionAutoFillDTO? autoFilData = await _employeePromotionsRepository.GetEmployeePromotionAutofillData(employeeId, companyId);
        return HttpStatusCodeResponse.SuccessResponse(autoFilData, string.Empty);
    }
}
