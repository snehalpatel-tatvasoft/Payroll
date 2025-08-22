using Microsoft.AspNetCore.Mvc;
using PalladiumPayroll.DTOs.DTOs.CheckInOut;

namespace PalladiumPayroll.Services.CheckInOut;

public interface ICheckInOutService
{
    Task<JsonResult> SaveClockInOut(CheckInOutRequesetDTO request);
}
