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
    public async Task<ActionResult> UpsertEmployeePromotions(EmployeePromotionsUpsertData request)
    {
        try
        {
            // if (request.EmployeePromotionId < 0)
            // {
            //     return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            // }
            return await _employeePromotionsService.UpsertEmployeePromotions(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.EmployeePromotions, ex.Message)
            );
        }
    }


    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteEmployeePromotion(long employeePromotionId, string userId)
    {
        try
        {
            if (employeePromotionId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.InvalidEmployeePromotionId);
            }

            return await _employeePromotionsService.DeleteEmployeePromotion(employeePromotionId, userId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.EmployeePromotions, ex.Message)
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeePromotions, ex.Message));
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeePromotions, ex.Message));
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
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.EmployeePromotions, ex.Message));
        }
    }
}
