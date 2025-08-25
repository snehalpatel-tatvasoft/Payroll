using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CheckInOut;
using PalladiumPayroll.DTOs.Miscellaneous;
using PalladiumPayroll.Repositories.CheckInOut;

namespace PalladiumPayroll.Services.CheckInOut;

public class CheckInOutService : ICheckInOutService
{
    private readonly ICheckInOutRepository _checkInOutRepository;

    public CheckInOutService(ICheckInOutRepository checkInOutRepository)
    {
        _checkInOutRepository = checkInOutRepository;
    }

    public async Task<JsonResult> SaveClockInOut(CheckInOutRequesetDTO request)
    {
        CheckInOutResultDTO? result = await _checkInOutRepository.SaveClockInOut(request);

        if (!result.IsSuccess)
        {
            return HttpStatusCodeResponse.InternalServerErrorResponse(result.Message);
        }

        return HttpStatusCodeResponse.SuccessResponse(string.Empty,result.Message);
    }

}
