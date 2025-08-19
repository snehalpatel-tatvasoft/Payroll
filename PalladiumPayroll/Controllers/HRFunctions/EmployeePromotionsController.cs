using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.EmployeePromotions;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.EmployeePromotions;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class EmployeePromotionsController : ControllerBase
{
    private readonly IEmployeePromotionsService _employeePromotionsService;

    public EmployeePromotionsController(IEmployeePromotionsService employeePromotionsService)
    {
        _employeePromotionsService = employeePromotionsService;
    }


    [HttpPost("[action]")]
    public async Task<ActionResult> AddEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        try
        {
            return await _employeePromotionsService.AddEmployeePromotion(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.EmployeePromotions)
            );
        }
    }

    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateEmployeePromotion(EmployeePromotionsUpsertData request)
    {
        try
        {
            if (request.EmployeePromotionsId <= 0 || request.EmployeePromotionsId == null)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            }
            return await _employeePromotionsService.UpdateEmployeePromotion(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.EmployeePromotions)
            );
        }
    }


    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeePromotion(long employeePromotionId)
    {
        try
        {
            if (employeePromotionId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            }

            return await _employeePromotionsService.DeleteEmployeePromotion(employeePromotionId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.EmployeePromotions)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeePromotionDropdownData(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeePromotionsService.GetEmployeePromotionDropdownData(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeePromotions)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeePromotions(long companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _employeePromotionsService.GetEmployeePromotions(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeePromotions)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeePromotionById(long promotionId)
    {
        try
        {
            if (promotionId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            }
            return await _employeePromotionsService.GetEmployeePromotionById(promotionId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeePromotions)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetEmployeePromotionAutofillData(long employeeId, long companyId)
    {
        try
        {
            if (employeeId <= 0 || companyId<=0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }
            return await _employeePromotionsService.GetEmployeePromotionAutofillData(employeeId,companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.EmployeePromotions)
            );
        }
    }
}
