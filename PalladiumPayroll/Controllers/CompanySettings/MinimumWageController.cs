using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.RequestDTOs.CompanySettings;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CompanySettings;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CompanySettings;

[ApiController]
[Route("api/[controller]")]
public class MinimumWageController : ControllerBase
{
    private readonly IMinimumWageService _minimumWageService;

    public MinimumWageController(IMinimumWageService minimumWageService)
    {
        _minimumWageService = minimumWageService;
    }


    [HttpPost("CreateMinimumWage")]
    public async Task<ActionResult> SaveMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            return await _minimumWageService.CreateMinimumWage(request);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Saving, ResponseMessages.MinimumWage, ex.Message)
            );
        }
    }


    [HttpGet("company/{companyId}")]
    public async Task<ActionResult> GetMinimumWagesByCompanyId(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Company Id is not found.");
            }

           return await _minimumWageService.GetMinimumWagesByCompanyId(companyId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Retrieving, ResponseMessages.MinimumWage, ex.Message));
        }
    }


    [HttpPatch("UpdateMinimumWage")]
    public async Task<ActionResult> UpdateMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            if (request.Id == null || request.Id == 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Wage Id is not found.");
            }
            return await _minimumWageService.UpdateMinimumWage(request);      
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.Exception, ActionType.Updating, ResponseMessages.MinimumWage, ex.Message));
        }
    }


    [HttpDelete("DeleteMinimumWage/{wageId}")]
    public async Task<ActionResult> DeleteMinimumWage(int wageId)
    {
        try
        {
            if (wageId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse("Wage Id is not found.");
            }

            return await _minimumWageService.DeleteMinimumWage(wageId);
        }
        catch (Exception ex)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.Exception, ActionType.Deleting, ResponseMessages.MinimumWage, ex.Message)
            );
        }
    }

}
