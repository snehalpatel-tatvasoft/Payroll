using System.Net;
using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.HRFunctions.CoidAccident;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.HRFunctions.CoidAccident;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.HRFunctions;

[ApiController]
[Route("api/[controller]")]
public class CoidAccidentController : ControllerBase
{
    private readonly ICoidAccidentService _coidAccidentService;

    public CoidAccidentController(ICoidAccidentService coidAccidentService)
    {
        _coidAccidentService = coidAccidentService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpsertCOIDAccident([FromBody] CoidAccidentRequestDTO request)
    {
        try
        {
            var result = await _coidAccidentService.UpsertCOIDAccident(request);
            return result;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                           string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, ResponseMessages.CoidAccident));
        }
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAccidentsByCompany([FromQuery] long companyId)
    {
        try
        {
            var result = await _coidAccidentService.GetAccidentsByCompanyId(companyId);
            return result;
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                           string.Format(ResponseMessages.ExceptionMessage, ActionType.Retrieving, ResponseMessages.CoidAccident));
        }
    }
    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteCoidAccident(long coidAccidentId)
    {
        try
        {
            return await _coidAccidentService.DeleteCoidAccident(coidAccidentId);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Deleting, ResponseMessages.CoidAccident));
        }
    }

}
