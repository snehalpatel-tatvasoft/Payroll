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


    [HttpPost("[action]")]
    public async Task<ActionResult> CreateMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            return await _minimumWageService.CreateMinimumWage(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.MinimumWage)
            );
        }
    }


    [HttpGet("[action]")]
    public async Task<ActionResult> GetMinimumWagesByCompanyId(int companyId)
    {
        try
        {
            if (companyId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.CompanyIdNotFound);
            }

            return await _minimumWageService.GetMinimumWagesByCompanyId(companyId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.MinimumWage));
        }
    }


    [HttpPut("[action]")]
    public async Task<ActionResult> UpdateMinimumWage(MinimumWageRequestDTO request)
    {
        try
        {
            if (request.Id == null || request.Id == 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.WageIdNotFound);
            }
            return await _minimumWageService.UpdateMinimumWage(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(string.Format(ResponseMessages.ExceptionMessage, ActionType.Updating, ResponseMessages.MinimumWage));
        }
    }


    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteMinimumWage(int wageId)
    {
        try
        {
            if (wageId <= 0)
            {
                return HttpStatusCodeResponse.NotFoundResponse(ResponseMessages.WageIdNotFound);
            }

            return await _minimumWageService.DeleteMinimumWage(wageId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.MinimumWage)
            );
        }
    }

}
