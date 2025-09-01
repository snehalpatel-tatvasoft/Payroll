using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CheckInOut;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Services.CheckInOut;
using static PalladiumPayroll.Helper.Constants.AppConstants;
using static PalladiumPayroll.Helper.Constants.AppEnums;

namespace PalladiumPayroll.Controllers.CheckInOut;

[ApiController]
[Route("api/[controller]")]

public class CheckInOutController : ControllerBase
{
    private readonly ICheckInOutService _checkInOutService;

    public CheckInOutController(ICheckInOutService checkInOutService)
    {
        _checkInOutService = checkInOutService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> SaveClockInOut(CheckInOutRequesetDTO request)
    {
        try
        {
            return await _checkInOutService.SaveClockInOut(request);
        }
        catch (Exception)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(
                string.Format(ResponseMessages.ExceptionMessage, ActionType.Saving, "Clock In/Out")
            );
        }
    }

}
